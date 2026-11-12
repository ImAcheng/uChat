namespace uChat_Desktop.DataModel {
    public class AccountDTO {
        public Guid AccountId { get; set; }
        public long AccountNo { get; set; }
        public string Username { get; set; }
        public string? Nickname { get; set; }
    }
}
