using System.Windows.Forms;
using uChat_Desktop.DataModel;

namespace uChat_Desktop.MainApp.Grid {
    public partial class ChatDisplay : UserControl {
        public readonly Guid ChatId;
        private readonly Guid? _CurMemberId = null;
        private readonly string _Name;

        public ChatDisplay(Guid chatId, Guid memberId, string name) {
            InitializeComponent();
            ChatId = chatId;
            _CurMemberId = memberId;
            _Name = name;
        }

        private void ChatDisplay_Load(object sender, EventArgs e) {
            lbName.Text = _Name;
        }
    }
}
