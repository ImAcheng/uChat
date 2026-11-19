namespace uChat_Desktop.MainApp.Components {
    public partial class MessageDisplayer : UserControl {
        public Guid? MessageId;
        public Guid SenderId;
        private string _Username;
        private bool _Continue;             // to represent it's the first text in one message section or not
        private string? _Text;
        // private List<Bitmap>? _Image;
        private bool _InBuffer;
        public readonly int ObjectIndex;    // represent it's index listed in view

        private static readonly Color _NormalColor = Color.White;
        private static readonly Color _InBufColor = Color.Gray;
        private static readonly Color _FailedColor = Color.Red;
        private static readonly Color _HoverBack = Color.FromArgb(72, 72, 72);
        private static readonly Color _DefaultBack = Color.FromArgb(64, 64, 64);

        public MessageDisplayer(Guid? messageId, Guid senderId, string username, bool useContinue, 
                                int objIndex, string? text = null, List<Bitmap>? images = null, 
                                bool inBuffer = false) {

            InitializeComponent();

            MessageId = messageId;
            SenderId = senderId;
            _Username = username;
            _Continue = useContinue;
            _Text = text;
            // _Image = images;
            _InBuffer = inBuffer;

            ObjectIndex = objIndex;
        }

        private void MessageDisplayer_Load(object sender, EventArgs e) {
            lbUsername.Text = _Username;
            lbMessage.Text = _Text;
            if (_InBuffer) lbMessage.ForeColor = _InBufColor;
            UpdateHeight();
            SetContinueTo(_Continue);
        }

        public void SetContinueTo(bool? useContinue = null) {
            _Continue = useContinue == null ? !_Continue : (bool)useContinue;

            lbUsername.Visible = !_Continue;
            imgAvatar.Visible = !_Continue;

            UpdateHeight();
        }

        public void UpdateText(string text) {
            _Text = text;
            lbMessage.Text = text;

            UpdateHeight();
        }

        public int MakeInvisible() {
            this.Hide();
            return ObjectIndex;
        }

        public void SetSendingStatus(bool isSuccess) {
            _InBuffer = false;
            lbMessage.ForeColor = isSuccess ? _NormalColor : _FailedColor;
        }

        bool isUpdatingHeight = false;
        public void UpdateHeight() {
            UpdateControlHeight();
        }

        private void UpdateControlHeight() {
            int padding = Padding.Top + Padding.Bottom;
            int usernameHeight = lbUsername.PreferredHeight;
            int textHeight = CalculateTextBoxHeight(lbMessage);
            int totalHeight = textHeight;
            if (!_Continue) totalHeight += usernameHeight;
            Height = totalHeight + padding;
        }

        private int CalculateTextBoxHeight(TextBox textBox) {
            int width = textBox.ClientSize.Width;

            if (width <= 0)
                return textBox.Height;

            TextFormatFlags flags =
                TextFormatFlags.WordBreak |
                TextFormatFlags.TextBoxControl |
                TextFormatFlags.NoPadding;

            Size textSize = TextRenderer.MeasureText(
                textBox.Text.Length == 0 ? " " : textBox.Text,
                textBox.Font,
                new Size(width, int.MaxValue),
                flags
            );

            return textSize.Height;
        }

        private void MessageDisplayer_SizeChanged(object sender, EventArgs e) {
            if (isUpdatingHeight) return;
            UpdateHeight();
            isUpdatingHeight = false;
        }

        public bool HighLighted = false;
        public void SetHighlight() {
            HighLighted = true;
            this.BackColor = _HoverBack;
            lbMessage.BackColor = _HoverBack;
        }

        public void SetDefault() {
            HighLighted = false;
            this.BackColor = _DefaultBack;
            lbMessage.BackColor = _DefaultBack;
        }
    }
}
