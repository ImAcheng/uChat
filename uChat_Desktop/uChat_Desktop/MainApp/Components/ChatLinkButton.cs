using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using uChat_Desktop.Lang;

namespace uChat_Desktop.MainApp.Components {
    public partial class ChatLinkButton : UserControl {
        private readonly Guid _ChatId;
        private string? _ChatTitle;
        private bool _isOnline;
        private Bitmap? _Avatar;
        private byte _ChatType;

        public ChatLinkButton(Guid chatID, bool isOnline, byte chatType, string? chatTitle, Bitmap? avatar) {
            InitializeComponent();
            _ChatId = chatID;
            _isOnline = isOnline;
            _ChatType = chatType;
            _ChatTitle = chatTitle;
            _Avatar = avatar;
        }

        private void ChatLinkButton_Load(object sender, EventArgs e) {
            lbChatTitle.Text = _ChatTitle ?? (_ChatType == 0 ? "User" : "Group Chat");

            if (_Avatar != null)
                imgProfilePicture.Image = _Avatar;
        }
    }
}
