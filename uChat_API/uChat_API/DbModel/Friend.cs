namespace uChat_API.DbModel {
    public class Friend {
        public Guid FriendId { get; set; }

        public Guid AccountId { get; set; }

        public Guid FriendAccountId { get; set; }

        public byte Type { get; set; } = 0;

        public byte Status { get; set; } = 0;

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }

        public virtual Account Account { get; set; } = null!;

        public virtual Account FriendAccount { get; set; } = null!;
    }
}
