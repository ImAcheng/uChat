using System.Text.Json.Serialization;

namespace uChat_Desktop.DataModel {
    internal class AccountDTO {
        public Guid AccountId { get; set; }
        public long AccountNo { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Nickname { get; set; }
        public string? AccountBio { get; set; }
        public byte GenderCode { get; set; }
    }
}
