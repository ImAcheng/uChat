using uChat_Desktop.Extention;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Grid {
    public partial class ChatDisplay : UserControl {
        public readonly Guid ChatId;
        private readonly Guid? _CurMemberId = null;
        private readonly string _Name;  // represent the chat name

        private Dictionary<Guid, MessageDisplayer> MessageDict = new Dictionary<Guid, MessageDisplayer>();

        public ChatDisplay(Guid chatId, Guid memberId, string name) {
            InitializeComponent();
            ChatId = chatId;
            _CurMemberId = memberId;
            _Name = name;
        }

        private async void ChatDisplay_Load(object sender, EventArgs e) {
            if (_CurMemberId == null) {
                uMessageBox.Show(FormMain.instance, "Oops, something went wrong while opening this chat.", AntdUI.TType.Error);
                this.Dispose();
                return;
            }

            lbName.Text = _Name;
            tbMessage.Multiline = true;

            // load history
            var history = await ChatService.LoadHistory(ChatId);
            history?.Data?.ForEach(x => AddMessage(x.MessageId, x.TextContent));
        }

        #region functions
        private DateTime _StartSendingTime = DateTime.Now;
        private int _SendingTime = 0;
        private async void _SendMsg() {
            // Force stop when users send too fast
            if (DateTime.Now - _StartSendingTime >= TimeSpan.FromSeconds(2)) {
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

            // create data & add to screen
            Guid msgId = Guid.NewGuid();
            string text = tbMessage.Text.TrimStart().TrimEnd();

            AddMessage(msgId, text);
            tbMessage.Clear();

            // this will firstly return if the message could be sent to server
            bool sent = await ChatService.SendMessage(ChatId, (Guid)_CurMemberId!, msgId, text);

            if (!sent) {
                MessageDict[msgId].SetSendingStatus(false);
            }
        }

        MessageDisplayer? _PrevAddedMsg = null;
        int _ContinueCount = 0;
        public void AddMessage(Guid msgId, string? text) {
            panelMessages.SuspendLayout();

            if (_CurMemberId == null) {
                panelMessages.ResumeLayout();
                return;
            }

            if (string.IsNullOrWhiteSpace(text)) {
                panelMessages.ResumeLayout();
                return;
            }

            // check if this message is created by same user & continious count <= 7
            bool useContinue = _PrevAddedMsg?.SenderId == _CurMemberId && ++_ContinueCount <= 7;
            if (!useContinue) _ContinueCount = 0;

            // create & add msg object
            var msgDisp = new MessageDisplayer(msgId, (Guid)_CurMemberId,
                        AccountService.LoggedAccount!.Username, useContinue,
                        _PrevAddedMsg?.ObjectIndex + 1 ?? 0, text: text) {
                Dock = DockStyle.Top
            };

            panelMessages.Controls.Add(msgDisp);
            MessageDict.Add(msgId, msgDisp);
            _PrevAddedMsg = msgDisp;

            msgDisp.BringToFront();
            panelMessages.ResumeLayout(true);

            // if (msgDisp.SenderId == _CurMemberId || panelMessages.AutoScrollOffset.Y == panelMessages.Max)
            panelMessages.ScrollControlIntoView(msgDisp);
        }
        #endregion

        #region send msg actions
        private void btnSendMsg_Click(object sender, EventArgs e) {
            _SendMsg();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && !e.Shift) _SendMsg();
        }
        #endregion
    }
}
