using System.Text.Json;
using uChat_Desktop.DataModel;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Pages {
    public partial class DirectMessagePage : UserControl {
        private readonly AccountDTO _Account;

        public DirectMessagePage(AccountDTO account) {
            InitializeComponent();
            _Account = account;
        }

        private async void btnFindUser_Click(object sender, EventArgs e) {
            var users = await AccountService.FindUsers(input1.Text);
            // MessageBox.Show(JsonSerializer.Serialize(users));
            panelFrdOrSrch.SuspendLayout();
            users?.ForEach(x => {
                var row = new UserResultRow(x.AccountId, x.Username) {
                    Parent = panelFrdOrSrch,
                    Dock = DockStyle.Top
                };
            });
            panelFrdOrSrch.ResumeLayout();
        }
    }
}
