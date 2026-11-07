namespace uChat_Desktop.MainApp.Components {
    public partial class UserResultRow : UserControl {
        private readonly Guid _ID;
        private readonly string _Username;
        private static readonly Color _DefaultBack = Color.FromArgb(60, 60, 60);
        private static readonly Color _HoverBack = Color.FromArgb(72, 72, 72);

        public UserResultRow(Guid accId, string uname) {
            InitializeComponent();
            _ID = accId;
            _Username = uname;
        }

        private void UserResultRow_Load(object sender, EventArgs e) {
            lbUsername.Text = _Username;

            foreach (Control ctrl in this.Controls) {
                ctrl.MouseEnter += UserResultRow_MouseEnter;
                ctrl.MouseLeave += UserResultRow_MouseLeave;
            }
        }

        private void UserResultRow_MouseLeave(object? sender, EventArgs e) {
            this.BackColor = _DefaultBack;
        }

        private void UserResultRow_MouseEnter(object? sender, EventArgs e) {
            this.BackColor = _HoverBack;
        }
    }
}
