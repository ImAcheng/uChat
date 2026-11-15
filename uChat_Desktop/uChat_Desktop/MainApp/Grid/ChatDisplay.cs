using uChat_Desktop.Extention;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Grid {
    public partial class ChatDisplay : UserControl {
        public readonly Guid ChatId;
        private readonly Guid? _CurMemberId = null;
        private readonly string _Name;

        private List<MessageDisplayer> LoadedMessages = new List<MessageDisplayer>();
        private Queue<MessageDisplayer> SendingBuffer = new Queue<MessageDisplayer>();
        private Dictionary<Guid, MessageDisplayer> MsgDict = new Dictionary<Guid, MessageDisplayer>();

        public ChatDisplay(Guid chatId, Guid memberId, string name) {
            InitializeComponent();
            ChatId = chatId;
            _CurMemberId = memberId;
            _Name = name;
        }

        private void ChatDisplay_Load(object sender, EventArgs e) {
            lbName.Text = _Name;
            tbMessage.Multiline = true;
            // MessageBox.Show($"Your member id is: {_CurMemberId}");
        }

        #region functions
        private DateTime _StartSendingTime = DateTime.Now, LastSendingTime = DateTime.Now;
        private int _SendingTime = 0;
        private void _SendMsg() {
            // if (DateTime.Now - LastSendingTime <= TimeSpan.FromMilliseconds(300)) return;
            // LastSendingTime = DateTime.Now;

            if (DateTime.Now - _StartSendingTime >= TimeSpan.FromSeconds(1)) {
                _StartSendingTime = DateTime.Now;
                _SendingTime = 0;
            }
            else {
                if (_SendingTime++ >= 5) {
                    uMessageBox.Show(FormMain.instance, "You're sending too fast!\nTake a rest!", 
                        AntdUI.TType.Warn);
                    tbMessage.Clear();
                    return;
                }
            }

            string text = tbMessage.Text.TrimStart().TrimEnd();
            AddMessage(text);
            tbMessage.Clear();
        }

        int _ContinueCount = 0;
        public void AddMessage(string text) {
            panelMessages.SuspendLayout();

            if (_CurMemberId == null) {
                panelMessages.ResumeLayout();
                return;
            }

            if (string.IsNullOrWhiteSpace(text)) {
                panelMessages.ResumeLayout();
                return;
            }

            var lastMessage = LoadedMessages.Count != 0 ? LoadedMessages.Last() : null;
            bool useContinue = lastMessage != null && lastMessage.SenderId == _CurMemberId;

            if (!useContinue || ++_ContinueCount >= 7) {
                useContinue = false;
                _ContinueCount = 0;
            }

            var msgDisp = new MessageDisplayer(null, (Guid)_CurMemberId,
                        AccountService.LoggedAccount!.Username, useContinue,
                        lastMessage?.ObjectIndex + 1 ?? 0, text: text) {
                Dock = DockStyle.Top
            };

            panelMessages.Controls.Add(msgDisp);
            LoadedMessages.Add(msgDisp);
            msgDisp.BringToFront();

            panelMessages.ResumeLayout(true);

            // if (msgDisp.SenderId == _CurMemberId || panelMessages.AutoScrollOffset.Y == panelMessages.Max)
            panelMessages.ScrollControlIntoView(msgDisp);
        }
        #endregion

        private void btnSendMsg_Click(object sender, EventArgs e) {
            _SendMsg();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && !e.Shift)
                _SendMsg();
        }
    }
}
