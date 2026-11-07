namespace uChat_API.DbModel {
    public class ServerMember {
        public Guid ServerMemberId { get; set; }

        public Guid AccountId { get; set; }

        public Guid ServerGroupId { get; set; }

        public string? Nickname { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }
    }
}
