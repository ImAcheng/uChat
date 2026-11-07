using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace uChat_Desktop.MainApp.Grid {
    public partial class FriendDisplay : UserControl {
        private readonly Guid _UserId;

        public FriendDisplay(Guid accountId) {
            InitializeComponent();
            _UserId = accountId;
        }
    }
}
