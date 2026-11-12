using uChat_Desktop.DataModel;
using uChat_Desktop.MainApp.Grid;

namespace uChat_Desktop.MainApp.Pages {
    public partial class DirectMessagePage : UserControl {
        private readonly AccountDTO _Account;

        public DirectMessagePage(AccountDTO account) {
            InitializeComponent();
            _Account = account;
        }

        FriendAndSearch FrdSrcGrid;

        private void DirectMessagePage_Load(object sender, EventArgs e) {
            FrdSrcGrid = new FriendAndSearch {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            FrdSrcGrid.Show();
        }
    }
}
