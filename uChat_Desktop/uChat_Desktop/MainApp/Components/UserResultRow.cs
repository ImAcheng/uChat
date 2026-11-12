using AntdUI;
using uChat_Desktop.Extention;
using uChat_Desktop.MainApp.Pages;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Components {
    public partial class UserResultRow : UserControl {
        private readonly Guid _ID;
        private readonly string _Username;
        private static readonly Color _DefaultBack = Color.FromArgb(64, 64, 64);
        private static readonly Color _HoverBack = Color.FromArgb(72, 72, 72);

        public UserResultRow(Guid accId, string uname) {
            InitializeComponent();
            _ID = accId;
            _Username = uname;
        }

        private void UserResultRow_Load(object sender, EventArgs e) {
            lbUsername.Text = _Username;
            SetDefault();
        }

        public bool HighLighted = false;

        public void SetHighlight() {
            SetColor(this, _HoverBack);
            panel1.Show();
            HighLighted = true;
        }

        public void SetDefault() {
            SetColor(this, _DefaultBack);
            panel1.Hide();
            HighLighted = false;
        }

        private void SetColor(Control control, Color color) {
            foreach (Control ctrl in control.Controls) {
                ctrl.BackColor = color;

                foreach (Control c in ctrl.Controls) {
                    SetColor(c, color);
                }
            }
        }

        private async void btnGoMsg_Click(object sender, EventArgs e) {
            var chat = await ChatService.OpenChatFromUser(_ID);

            if (chat == null) return;
            if (chat.Chat == null) {
                uMessageBox.Show(FormMain.instance, chat.Message, TType.Error);
                return;
            }

            DirectMessagePage.Instance.AddUserRow(new ChatLinkButton(chat.Chat.ChatId, chat.Chat.ChatName ?? "NAME_LOST"));
        }
    }
}
