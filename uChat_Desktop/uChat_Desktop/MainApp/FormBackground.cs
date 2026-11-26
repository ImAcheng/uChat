using AntdUI;
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

        private System.Windows.Forms.Timer ApiChecksTimer = new() { Interval = 5000 };
        private System.Windows.Forms.Timer AnimationTimer = new() { Interval = 500 };
        private System.Windows.Forms.Timer IpInputTimer = new() { Interval = 300 };

        private FormMain? formMain;

        // to store if app was started already to prevent re-open a new window
        bool _IsAppStarted = false;

        private string _SubText = "";

        private async void FormBackground_Load(object sender, EventArgs e) {
            FileService.Init();

            ApiChecksTimer.Tick += ApiChecksTimer_Tick;
            AnimationTimer.Tick += AnimationTimer_Tick;
            IpInputTimer.Tick += IpInputTimer_Tick;

            _AddDraggingEvent(this.Controls);

            string ipFileFp = FileService.MakePath(FileService.GetPathOf(FileService.CommanPath.Server));
            if (FileService.ConfirmFile(ipFileFp)) {
                using var sw = new StreamReader(ipFileFp);
                var ipCtx = await sw.ReadLineAsync();
                ipCtx = ipCtx?.Trim();

                if (!string.IsNullOrWhiteSpace(ipCtx)) {
                    ApiService.ApiAddress = ipCtx!;
                    ApiService.RestApiUri = new($"http://{ipCtx!}");
                    ApiService.ApiClient = new() { BaseAddress = ApiService.RestApiUri };
                    tbIP.Text = ipCtx;
                }
            }

            lbConn.Text = _SubText;
        }

        byte _DotsCount = 0;
        private void AnimationTimer_Tick(object? sender, EventArgs e) {
            lbConn.Text = _SubText + string.Join("", Enumerable.Repeat(".", _DotsCount));
            ++_DotsCount; _DotsCount %= 4;
        }

        #region connection checker
        bool _IsCheckingApiConn = false;
        ConnectionStatus _ConnStatus = ConnectionStatus.Initializing;
        private async void ApiChecksTimer_Tick(object? sender, EventArgs e) {
            if (_IsCheckingApiConn) return;
            _IsCheckingApiConn = true;

            switch (_ConnStatus) {
                case ConnectionStatus.RetryingWhenInit or ConnectionStatus.RetryingWhenLauch:
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
            _IsCheckingApiConn = true;

            ApiService.ApiClient = new HttpClient() { BaseAddress = ApiService.RestApiUri };

            // try to connect to websockets
            _ConnStatus = ConnectionStatus.LauchingServices;
            if (!await ApiService.TryStartWebSockets()) {
                _UpdateSubText("Retrying");
                _ConnStatus = ConnectionStatus.RetryingWhenLauch;
                _IsCheckingApiConn = false;
                return false;
            }

            // all internet services lauched, then start app
            _UpdateSubText("Lauching");
            _ConnStatus = ConnectionStatus.Connected;

            _IsCheckingApiConn = false;
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

        #region IP Input & Update
        private async void IpInputTimer_Tick(object? sender, EventArgs e) {
            string? target = tbIP.Text;
            bool valid = ApiService.TryConvertIP(target, out target);

            if (!valid) {
                tbIP.Status = TType.Error;
                return;
            }

            try {
                ApiService.ApiAddress = target!;
                ApiService.RestApiUri = new($"http://{target!}");
                ApiService.ApiClient = new() { BaseAddress = ApiService.RestApiUri };
            }
            catch {
                tbIP.Status = TType.Error;
                return;
            }

            tbIP.Status = TType.Success;

            var fp = FileService.MakePath(FileService.GetPathOf(FileService.CommanPath.Server));
            FileService.ConfirmFile(fp);
            using var sr = new StreamWriter(fp);
            await sr.WriteLineAsync(target);
        }

        private void tbIP_TextChanged(object sender, EventArgs e) {
            IpInputTimer.Stop();
            IpInputTimer.Start();
        }
        #endregion

        private async void btnStartConnect_Click(object sender, EventArgs e) {
            if (tbIP.Status != TType.Success) return;

            AnimationTimer.Enabled = true;
            btnStartConnect.Loading = true;
            _UpdateSubText("Connecting");

            // connect to services
            bool connTest = await _InitConnectionCheck();
            btnStartConnect.Loading = false;

            if (!connTest) {
                _UpdateSubText("Connection Failed");
                AnimationTimer.Stop();
                return;
            }

            lbInputHint.Hide();
            tbIP.Hide();
            btnStartConnect.Hide();
            
            ApiChecksTimer.Enabled = true;
            await _StartApp();
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
