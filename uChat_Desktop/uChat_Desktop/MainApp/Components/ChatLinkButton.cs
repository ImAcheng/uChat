namespace uChat_Desktop.MainApp.Components {
    public partial class ChatLinkButton : UserControl {
        private readonly Guid _ChatId;
        private string _ChatTitle;
        private bool _isOnline;
        private Bitmap? _Avatar;

        public ChatLinkButton(Guid chatID, string chatTitle, bool isOnline = false, Bitmap? avatar = null) {
            InitializeComponent();
            _ChatId = chatID;
            _isOnline = isOnline;
            _ChatTitle = chatTitle;
            _Avatar = avatar;
        }

        private void ChatLinkButton_Load(object sender, EventArgs e) {
            lbChatTitle.Text = _ChatTitle;
        }
    }
}
