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

        public static DirectMessagePage Instance;
        FriendAndSearch? FrdSrcGrid;

        private System.Windows.Forms.Timer RenderTimer = new System.Windows.Forms.Timer() {
            Interval = 50,
            Enabled = true
        };

        private async void DirectMessagePage_Load(object sender, EventArgs e) {
            Instance = this;
            FrdSrcGrid = new FriendAndSearch {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            FrdSrcGrid.Show();

            var chats = await ChatService.GetChats();
            if (chats != null) {
                chats.Chat!.ForEach(x => {
                    AddUserRow(new ChatLinkButton(x.ChatId, x.ChatName));
                });
            }

            RenderTimer.Tick += (_, _) => {
                bool defaultSet = false, hoverSet = false;
                foreach (var item in panelActualChats.Controls.OfType<ChatLinkButton>()) {
                    if (item.ClientRectangle.Contains(item.PointToClient(Cursor.Position))) {
                        item.SetHighlight();
                        hoverSet = true;
                    }
                    else if (item.IsHover) {
                        item.SetDefault();
                        defaultSet = true;
                    }

                    if (defaultSet & hoverSet) break;
                }
            };
        }

        public void AddUserRow(ChatLinkButton row) {
            row.Parent = panelActualChats;
            row.SendToBack();
        }

        public void SwitchToMessage(Guid chatId) {
            FrdSrcGrid?.Dispose();
            FrdSrcGrid = null;
        }
    }
}
