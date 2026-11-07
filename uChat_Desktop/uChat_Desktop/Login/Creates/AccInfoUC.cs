using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using uChat_Desktop.Lang;
using uChat_Desktop.MainApp;
using uChat_Desktop.Service;

namespace uChat_Desktop.Login.Creates {
    public partial class AccInfoUC : UserControl {
        public AccInfoUC() {
            InitializeComponent();
        }

        private void AccInfoUC_Load(object sender, EventArgs e) {
            lbFailedMessage.Text = string.Empty;
        }

        private void btnNext_Click(object sender, EventArgs e) {
            string username = tbUsername.Text.Trim().ToLower();
            string password = tbPassword.Text.Trim();
            string cPassword = tbPasswordConfirm.Text.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
                lbFailedMessage.Text = Langs.uname_pwd_empty;
                return;
            }

            string usernamePattern = @"^[a-zA-Z0-9._]{2,}$";
            if (!Regex.IsMatch(username, usernamePattern)) {
                lbFailedMessage.Text = Langs.bad_username;
                return;
            }

            if (!password.Equals(cPassword)) {
                lbFailedMessage.Text = Langs.pwd_not_match;
                return;
            }

            FormCreateAccount.Instance._Username = username;
            FormCreateAccount.Instance._Password = password;

            FormCreateAccount.Instance.NextPage();
        }

        private void btnReturn_Click(object sender, EventArgs e) {
            FormCreateAccount.Instance.DialogResult = DialogResult.Abort;
            FormCreateAccount.Instance.Dispose();
        }
    }
}
