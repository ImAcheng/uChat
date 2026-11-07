namespace uChat_API.DbModel {
    public class ChatRoomMember {
        public Guid ChatRoomMemberId { get; set; }

        public Guid AccountId { get; set; }

        public Guid ChatRoomId { get; set; }

        public string? Nickname { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }
    }
}
