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
        private FriendAndSearch? _FrdSrcGrid;
        private ChatDisplay? _ChatPage;

        private Guid? _CurChatId = null;

        private System.Windows.Forms.Timer RenderTimer = new System.Windows.Forms.Timer() {
            Interval = 16,
            Enabled = true
        };

        private async void DirectMessagePage_Load(object sender, EventArgs e) {
            Instance = this;
            _FrdSrcGrid = new FriendAndSearch {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            _FrdSrcGrid.Show();

            var chats = await ChatService.GetChats();
            panelActualChats.SuspendLayout();
            if (chats != null) {
                chats.Chat!.ForEach(x => {
                    AddUserRow(new ChatLinkButton(x.ChatId, x.ChatMemberId, x.ChatName ?? "User (Data lost)"), false);
                });
            }
            panelActualChats.ResumeLayout(true);

            // chat link buttons hover detect
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

        /// <summary>
        /// This is designed to add user rows to the panel correctly.
        /// This function detects if there's an exists one first.
        /// </summary>
        public void AddUserRow(ChatLinkButton row, bool useSuspend = true) {
            if (panelActualChats.Controls.OfType<ChatLinkButton>().Any(x => x.ChatId == row.ChatId)) {
                row.Dispose();
                return;
            }

            if (useSuspend)
                panelActualChats.SuspendLayout();

            row.Parent = panelActualChats;
            row.Dock = DockStyle.Top;
            row.SendToBack();

            if (useSuspend)
                panelActualChats.ResumeLayout(true);
        }

        public async void SwitchToMessage(Guid chatId, Guid memberId, string name) {
            if (chatId == _CurChatId) return;

            _CurChatId = chatId;

            _ChatPage?.Dispose();
            _ChatPage = new ChatDisplay(chatId, memberId, name) {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            _ChatPage.BringToFront();

            _FrdSrcGrid?.Dispose();
            _FrdSrcGrid = null;
        }

        private void btnFndUsers_Click(object sender, EventArgs e) {
            if (_FrdSrcGrid != null) return;
            _FrdSrcGrid = new FriendAndSearch() {
                Parent = panelMainContainer,
                Dock = DockStyle.Fill
            };
            _FrdSrcGrid.BringToFront();

            _ChatPage?.Dispose();
            _ChatPage = null;
            _CurChatId = null;
        }

        private void btnFriends_Click(object sender, EventArgs e) {

        }
    }
}
