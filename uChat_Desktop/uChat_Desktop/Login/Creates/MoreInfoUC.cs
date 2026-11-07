using uChat_Desktop.Extention;
using uChat_Desktop.Service;

namespace uChat_Desktop.Login.Creates {
    public partial class MoreInfoUC : UserControl {
        public MoreInfoUC() {
            InitializeComponent();
        }

        private async void btnCreate_Click(object sender, EventArgs e) {
            btnCreate.Loading = true;

            if (cbGender.SelectedValue == null) {
                btnCreate.Loading = false;
                uMessageBox.Show(FormCreateAccount.Instance, "Please select your gender.", AntdUI.TType.Warn);
                return;
            }

            string nickname = tbNickname.Text.Trim();
            byte gender = (byte)(cbGender.Items.IndexOf(cbGender.SelectedValue) % 256); // to prevent some genius from doing sth amazing

            var result = await AccountService.CallCreateAccount(new CreateAccountRequest() {
                username = FormCreateAccount.Instance._Username,
                nickname = nickname.Length == 0 ? null : nickname,
                password = FormCreateAccount.Instance._Password,
                genderCode = gender
            });

            btnCreate.Loading = false;

            if (result == null)
                return;

            if (!result.success) {
                uMessageBox.Show(FormCreateAccount.Instance, result.message, AntdUI.TType.Error);
            }
            else {
                uMessageBox.Show(FormCreateAccount.Instance, "Account created!", AntdUI.TType.Success);
                FormCreateAccount.Instance.DialogResult = DialogResult.OK;
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e) {
            FormCreateAccount.Instance.PreviousPage();
        }

        private void btnReturn_Click(object sender, EventArgs e) {
            FormCreateAccount.Instance.DialogResult = DialogResult.Abort;
            FormCreateAccount.Instance.Dispose();
        }

        private void cbGender_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e) {
            cbGender.Text = cbGender.SelectedValue?.ToString() ?? string.Empty;
        }
    }
}
