using uChat_Desktop.MainApp.Pages;

namespace uChat_Desktop.MainApp.Components {
    public partial class ChatLinkButton : UserControl {
        public readonly Guid ChatId;
        private string _ChatTitle;
        private bool _isOnline;
        private Bitmap? _Avatar;

        private static readonly Color _DefaultBack = Color.FromArgb(64, 64, 64);
        private static readonly Color _HoverBack = Color.FromArgb(72, 72, 72);

        public ChatLinkButton(Guid chatID, string chatTitle, bool isOnline = false, Bitmap? avatar = null) {
            InitializeComponent();
            ChatId = chatID;
            _isOnline = isOnline;
            _ChatTitle = chatTitle;
            _Avatar = avatar;

            SetEvent(this);
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

        private void SetEvent(Control control) {
            control.Click += Control_Click;
            foreach (Control ctrl in control.Controls)
                SetEvent(ctrl);
        }

        private void Control_Click(object? sender, EventArgs e) {
            DirectMessagePage.Instance.SwitchToMessage(ChatId, _ChatTitle);
        }
    }
}
