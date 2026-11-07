using AntdUI;
using uChat_Desktop.Lang;
using uChat_Desktop.Login;
using uChat_Desktop.Service;

namespace uChat_Desktop {
    public partial class FormLogin : Window {
        public FormLogin() {
            InitializeComponent();
        }

        public static FormLogin Instance;

        private void FormLogin_Load(object sender, EventArgs e) {
            Instance = this;
            // components setup
            lbFailedMessage.Text = string.Empty;
            _SetLang();
        }

        #region functions
        private void _SetLang() {
            lbUsername.Text = Langs.username;
            lbPassword.Text = Langs.password;

            btnLogin.Text = Langs.login;
            btnExit.Text = Langs.exit;

            llb_go_create_acc.Text = Langs.no_acc_go_create;
        }
        #endregion

        private async void btnLogin_Click(object sender, EventArgs e) {
            string username = tbUsername.Text.Trim();
            string password = tbPassword.Text.Trim();

            btnLogin.Loading = true;
            var result = await AccountService.LoginTo(username, password);
            btnLogin.Loading = false;

            if (result == null || !result.success) {
                lbFailedMessage.Text = result?.message ?? "Login failed.";
                return;
            }

            if (result.success)
                DialogResult = DialogResult.OK;
        }

        private void btnExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void llb_go_create_acc_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            using var cForm = new FormCreateAccount();
            cForm.ShowDialog();
        }
    }
}
