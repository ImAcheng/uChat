using System.Windows.Forms;
using uChat_Desktop.DataModel;

namespace uChat_Desktop.MainApp.Grid {
    public partial class ChatDisplay : UserControl {
        public readonly Guid ChatId;
        private readonly string _Name;

        public ChatDisplay(Guid chatId, string name) {
            InitializeComponent();
            ChatId = chatId;
            _Name = name;
        }

        private void ChatDisplay_Load(object sender, EventArgs e) {
            lbName.Text = _Name;
        }
    }
}
