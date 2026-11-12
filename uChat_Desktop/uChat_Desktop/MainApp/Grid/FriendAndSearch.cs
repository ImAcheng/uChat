using uChat_Desktop.DataModel;
using uChat_Desktop.Extention;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Grid {
    public partial class FriendAndSearch : UserControl {
        private readonly AccountDTO _CurAccount;

        public FriendAndSearch() {
            InitializeComponent();
            if (AccountService.LoggedAccount == null) {
                uMessageBox.Show(FormMain.instance,
                    "Oops! It seems like that you haven't logged in.\nPlease login to use uChat.",
                    AntdUI.TType.Error);
                FormMain.instance.Close();
                return;
            }

            _CurAccount = AccountService.LoggedAccount;
        }

        private FriendDisplay frdDisplay;

        private System.Windows.Forms.Timer RenderTimer = new System.Windows.Forms.Timer() {
            Interval = 16,
            Enabled = true
        };

        private void FriendAndSearch_Load(object sender, EventArgs e) {
            frdDisplay = new FriendDisplay(_CurAccount.AccountId);

            RenderTimer.Tick += (_, _) => {
                bool defaultSet = false, highLightSet = false;
                foreach (UserResultRow item in panelFrdOrSrch.Controls.OfType<UserResultRow>()) {
                    if (item.ClientRectangle.Contains(item.PointToClient(Cursor.Position))) {
                        item.SetHighlight();
                        highLightSet = true;
                    }
                    else if (item.HighLighted) {
                        item.SetDefault();
                        defaultSet = true;
                    }

                    if (defaultSet & highLightSet) break;
                }
            };
        }

        private async void btnFindUsers_Click(object sender, EventArgs e) {
            if (string.IsNullOrWhiteSpace(input1.Text)) return;

            btnFindUsers.Loading = true;
            
            var users = await AccountService.FindUsers(input1.Text);
            panelFrdOrSrch.Controls.Clear();

            panelFrdOrSrch.SuspendLayout();
            users?.ForEach(x => {
                var row = new UserResultRow(x.AccountId, x.Username) {
                    Parent = panelFrdOrSrch,
                    Dock = DockStyle.Top
                };
            });
            panelFrdOrSrch.ResumeLayout();

            btnFindUsers.Loading = false;
        }
    }
}
