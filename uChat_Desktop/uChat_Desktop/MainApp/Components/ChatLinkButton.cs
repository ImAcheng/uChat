namespace uChat_Desktop.MainApp.Components {
    public partial class ChatLinkButton : UserControl {
        private readonly Guid _ChatId;
        private string _ChatTitle;
        private bool _isOnline;
        private Bitmap? _Avatar;

        private static readonly Color _DefaultBack = Color.FromArgb(64, 64, 64);
        private static readonly Color _HoverBack = Color.FromArgb(72, 72, 72);

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

        public bool IsHover = false;

        public void SetHighlight() {
            panel1.Back = _HoverBack;
            lbChatTitle.BackColor = _HoverBack;
            IsHover = true;
        }

        public void SetDefault() {
            panel1.Back = _DefaultBack;
            lbChatTitle.BackColor = _DefaultBack;
            IsHover = false;
        }
    }
}
