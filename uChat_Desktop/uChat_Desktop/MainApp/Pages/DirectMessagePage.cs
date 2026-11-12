using uChat_Desktop.DataModel;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.MainApp.Grid;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Pages {
    public partial class DirectMessagePage : UserControl {
        private readonly AccountDTO _Account;

        public DirectMessagePage(AccountDTO account) {
            InitializeComponent();
            _Account = account;
        }

        FriendAndSearch FrdSrcGrid;

        private async void DirectMessagePage_Load(object sender, EventArgs e) {
            FrdSrcGrid = new FriendAndSearch {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            FrdSrcGrid.Show();

            var chats = await ChatService.GetChats();
            if (chats != null) {
                chats.Chat!.ForEach(x => {
                    new ChatLinkButton(x.ChatId, x.ChatName) {

                    };
                });
            }
        }
    }
}
