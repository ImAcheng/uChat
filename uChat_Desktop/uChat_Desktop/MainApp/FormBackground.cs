using System.Diagnostics;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp {
    /// <summary>
    /// Starting Services & Background Services
    /// </summary>
    public partial class FormBackground : Form {
        public FormBackground() {
            InitializeComponent();
        }

        private System.Windows.Forms.Timer ApiChecksTimer = 
            new System.Windows.Forms.Timer() { Interval = 5000 };

        private System.Windows.Forms.Timer AnimationTimer = 
            new System.Windows.Forms.Timer() { Interval = 500 };

        private FormMain? formMain;

        // to store if app was started already to prevent re-open a new window
        bool _IsAppStarted = false;

        private string _SubText = "Connecting";

        private async void FormBackground_Load(object sender, EventArgs e) {
            FileService.Init();

            ApiChecksTimer.Tick += ApiChecksTimer_Tick;
            ApiChecksTimer.Enabled = true;
            AnimationTimer.Tick += AnimationTimer_Tick;
            AnimationTimer.Enabled = true;

            _AddDraggingEvent(this.Controls);

            lbConn.Text = _SubText;

            // I put a delay here bcuz
            // I want the splash screen to be shown correctly.
            // Also use task to prevent ui traffic problem
            await Task.Run(() => {
                Thread.Sleep(500);
            });

            if (!await _InitConnectionCheck()) {
                return;
            }

            await _StartApp();
        }

        byte _DotsCount = 0;
        private void AnimationTimer_Tick(object? sender, EventArgs e) {
            _DotsCount %= 4;
            lbConn.Text = _SubText + string.Join("", Enumerable.Repeat(".", _DotsCount));
            ++_DotsCount;
        }

        #region connection checker
        bool _IsCheckingApiConn = false;
        ConnectionStatus _ConnStatus = ConnectionStatus.Initializing;
        private async void ApiChecksTimer_Tick(object? sender, EventArgs e) {
            if (_IsCheckingApiConn) return;
            _IsCheckingApiConn = true;

            switch (_ConnStatus) {
                case ConnectionStatus.Initializing or ConnectionStatus.RetryingWhenInit:
                    // check if application can technically ping API first
                    if (!await _InitConnectionCheck()) {
                        _UpdateSubText("Retrying");
                        _ConnStatus = ConnectionStatus.RetryingWhenInit;
                        break;
                    }

                    // try to connect to websockets
                    _ConnStatus = ConnectionStatus.LauchingServices;
                    if (!await ApiService.TryStartWebSockets()) {
                        _UpdateSubText("Retrying");
                        _ConnStatus = ConnectionStatus.RetryingWhenLauch;
                        break;
                    }

                    // all internet services lauched, then start app
                    _UpdateSubText("Lauching");
                    _ConnStatus = ConnectionStatus.Connected;
                    await _StartApp();
                    break;

                case ConnectionStatus.RetryingWhenLauch:
                    if (!await ApiService.TryStartWebSockets()) {
                        break;
                    }

                    _ConnStatus = ConnectionStatus.Connected;
                    await _StartApp();

                    break;


                case ConnectionStatus.Connected:
                    if (!await AccountService.UpdateAccountOnlineStatus()) {
                        _UpdateSubText("Reconnecting");
                        _ConnStatus = ConnectionStatus.RetryingAfterConnected;
                        formMain?.Hide();
                        _ShowThis();
                        await ApiService.CloseAllWebSockets();
                    }

                    break;

                case ConnectionStatus.RetryingAfterConnected:
                    if (!await ApiService.TryStartWebSockets()) {
                        break;
                    }

                    _ConnStatus = ConnectionStatus.Connected;
                    formMain?.Show();
                    _HideThis();

                    break;

            }

            _IsCheckingApiConn = false;
        }

        private async Task<bool> _InitConnectionCheck() {
            ApiService.ApiClient = new HttpClient() { BaseAddress = ApiService.RestApiUri };

            try {
                await ApiService.ApiClient.GetAsync(string.Empty);
            }
            catch (HttpRequestException) {
                return false;
            }

            return true;
        }
        #endregion

        public async void RestartApp() {
            formMain?.Dispose();
            formMain = null;
            await _StartApp();
        }

        private async Task _StartApp() {
            if (_IsAppStarted) return;

            _IsAppStarted = true;
            Debug.WriteLine("[Debug] App Started.");

            if (!await AccountService.CheckSavedLogin()) {
                using var login = new FormLogin();
                _HideThis();
                if (login.ShowDialog() != DialogResult.OK) {
                    Application.Exit();
                    return;
                }
            }

            _StartMainApp();
            _HideThis();
        }

        private void _StartMainApp() {
            formMain = new FormMain();
            formMain?.Show();

            if (formMain != null) {
                formMain.TopMost = true;
                formMain.TopMost = false;
            }
        }

        private void _HideThis() {
            this.Hide();
            AnimationTimer.Stop();
            _DotsCount = 0;
        }

        private void _ShowThis() {
            this.Show();
            this.TopMost = true;
            this.TopMost = false;
            AnimationTimer.Start();
        }

        private void _UpdateSubText(string text) {
            _DotsCount = 0;
            lbConn.Text = text;
            _SubText = text;
        }

        #region process dragging
        private void _AddDraggingEvent(Control.ControlCollection controls) {
            foreach (Control c in controls) {
                if (c.HasChildren) {
                    _AddDraggingEvent(c.Controls);
                }

                c.MouseDown += Form_MouseDown;
                c.MouseUp += Form_MouseUp;
                c.MouseMove += Form_MouseMove;
            }
        }

        bool _IsMsBtnDown = false;
        Point _StartPos;
        private void Form_MouseDown(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                _IsMsBtnDown = true;
                _StartPos = e.Location;
            }
        }

        private void Form_MouseUp(object? sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left)
                _IsMsBtnDown = false;
        }

        private void Form_MouseMove(object? sender, MouseEventArgs e) {
            if (!_IsMsBtnDown) return;

            this.Location = new Point(
                this.Location.X + (e.X - _StartPos.X),
                this.Location.Y + (e.Y - _StartPos.Y)
            );
        }
        #endregion

        private async void FormBackground_FormClosing(object sender, FormClosingEventArgs e) {
            await ApiService.CloseAllWebSockets();
        }
    }

    internal enum ConnectionStatus {
        Initializing,
        RetryingWhenInit,
        LauchingServices,
        RetryingWhenLauch,
        Connected,
        RetryingAfterConnected
    }
}
