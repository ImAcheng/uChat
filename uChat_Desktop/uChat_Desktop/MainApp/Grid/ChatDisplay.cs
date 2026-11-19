using uChat_Desktop.Extention;
using uChat_Desktop.MainApp.Components;
using uChat_Desktop.Service;

namespace uChat_Desktop.MainApp.Grid {
    public partial class ChatDisplay : UserControl {
        public readonly Guid ChatId;
        private readonly Guid _CurMemberId;
        private readonly string _Name;  // represent the chat name

        private Dictionary<Guid, MessageDisplayer> MessageDict = new Dictionary<Guid, MessageDisplayer>();
        private System.Windows.Forms.Timer _RenderTimer = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer _UpdateTimer = new System.Windows.Forms.Timer();

        public ChatDisplay(Guid chatId, Guid memberId, string name) {
            InitializeComponent();
            ChatId = chatId;
            _CurMemberId = memberId;
            _Name = name;
        }

        private async void ChatDisplay_Load(object sender, EventArgs e) {
            lbName.Text = _Name;
            tbMessage.Multiline = true;

            _RenderTimer.Interval = 50;
            _RenderTimer.Tick += (_, _) => {
                bool defaultSet = false, highLightSet = false;
                foreach (MessageDisplayer item in MessageDict.Values) {
                    if (item.IsDisposed) break;
                    if (item.ClientRectangle.Contains(item.PointToClient(Cursor.Position))) {
                        item.SetHighlight();
                        highLightSet = true;
                    }
                    else if (item.HighLighted) {
                        item.SetDefault();
                        defaultSet = true;
                    }

                    if (defaultSet & highLightSet) break;
                }
            };
            _RenderTimer.Enabled = true;

            // load history
            var history = await ChatService.LoadHistory(ChatId);
            panelMessages.SuspendLayout();
            history?.Data?.ForEach(x => AddMessage(x.MessageId, x.TextContent, useSuspend: false));
            panelMessages.ResumeLayout(true);

            _UpdateTimer.Interval = 1000;
            _UpdateTimer.Tick += _UpdateTimer_Tick;
            _UpdateTimer.Enabled = true;
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

            AddMessage(msgId, text, inBuffer: true);
            tbMessage.Clear();

            // this will firstly return if the message could be sent to server
            var result = await ChatService.SendMessage(ChatId, _CurMemberId, msgId, text);

            MessageDict.TryGetValue(msgId, out var msgObj);
            if (result == null || !result.Success)
                msgObj?.SetSendingStatus(false);
            else
                msgObj?.SetSendingStatus(true);
        }

        MessageDisplayer? _PrevAddedMsg = null;
        int _ContinueCount = 0;
        public void AddMessage(Guid msgId, string? text, bool inBuffer = false, bool useSuspend = true) {
            if (MessageDict.ContainsKey(msgId)) return;

            if (useSuspend)
                panelMessages.SuspendLayout();

            if (string.IsNullOrWhiteSpace(text)) {
                panelMessages.ResumeLayout();
                return;
            }

            // check if this message is created by same user & continious count <= 7
            bool useContinue = _PrevAddedMsg?.SenderId == _CurMemberId && ++_ContinueCount <= 7;
            if (!useContinue) _ContinueCount = 0;

            // create & add msg object
            var msgDisp = new MessageDisplayer(msgId, _CurMemberId,
                        AccountService.LoggedAccount!.Username, useContinue,
                        _PrevAddedMsg?.ObjectIndex + 1 ?? 0, text: text, inBuffer: inBuffer) {
                Dock = DockStyle.Top
            };

            panelMessages.Controls.Add(msgDisp);
            MessageDict.Add(msgId, msgDisp);
            _PrevAddedMsg = msgDisp;

            msgDisp.BringToFront();

            if (useSuspend)
                panelMessages.ResumeLayout(true);

            // if (msgDisp.SenderId == _CurMemberId || panelMessages.AutoScrollOffset.Y == panelMessages.Max)
            panelMessages.ScrollControlIntoView(msgDisp);
        }
        #endregion

        private async void _UpdateTimer_Tick(object? sender, EventArgs e) {
            var newData = await ChatService.GetMessages(ChatId, _CurMemberId);
            newData?.Data?.ForEach(x => {
                AddMessage(x.ChatId, x.TextContent);
            });
        }

        #region send msg actions
        private void btnSendMsg_Click(object sender, EventArgs e) {
            _SendMsg();
        }

        private void tbMessage_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter && !e.Shift) _SendMsg();
        }
        #endregion

        #region window process
        public void SuspenMessagePanel() {
            panelMessages.SuspendLayout();
        }

        public void ResumeMessagePanel() {
            panelMessages.ResumeLayout(true);
        }
        #endregion
    }
}