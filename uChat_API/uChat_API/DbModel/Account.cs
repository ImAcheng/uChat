using System.Text.Json.Serialization;

namespace uChat_API.DbModel {
    public class Account {
        public Guid AccountId { get; set; }
        public long AccountNo { get; set; }
        public string Email { get; set; } // alr, this is given up
        public string Username { get; set; } // I don't think this can be empty str
        public string Password { get; set; } // I don't think this can be empty str, either
        public string? Nickname { get; set; } = string.Empty;
        public string? AccountBio { get; set; } = string.Empty;
        public byte GenderCode { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset? UpdateDateTime { get; set; }
        public DateTimeOffset? LastOnlineDateTime { get; set; }

        // foreign key
        public virtual StaffAccount? StaffAccount { get; set; }
    }

    public class AccountDTO {
        public Guid AccountId { get; set; }
        public long AccountNo { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? Nickname { get; set; }
        public string? AccountBio { get; set; }
        public byte GenderCode { get; set; }

        public AccountDTO(Account _acc) {
            AccountId = _acc.AccountId;
            AccountNo = _acc.AccountNo;
            Nickname = _acc.Nickname;
            AccountBio = _acc.AccountBio;
            GenderCode = _acc.GenderCode;
        }
    }
}
