namespace uChat_API.DbModel {
    public class ChatRoom {
        public Guid ChatRoomId { get; set; }

        /// <summary>
        /// 0: One-to-One Chat
        /// 1: Normal Group Chat
        /// 2: Server-Affiliated Chat
        /// </summary>
        public byte ChatRoomType { get; set; }

        public Guid? ServerGroupId { get; set; }

        public string? ChatName { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }

        /// <summary>
        /// Last Modified Time (including new msg, reaction, etc. for type 0 & 1 only)
        /// </summary>
        public DateTimeOffset? UpdateDateTime { get; set; }

        public virtual ICollection<ChatRoomMember> ChatRoomMembers { get; set; }
            = new List<ChatRoomMember>();
    }

    public class ChatDisplayDTO {
        public Guid ChatId { get; set; }
        public Guid ChatMemberId { get; set; }
        public string? ChatName { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset? UpdateDateTime { get; set; }
    }

    public class ChatDTO {
        public Guid ChatId { get; set; }
        public Guid ChatMemberId { get; set; }
        public string? ChatName { get; set; }
        public byte ChatType { get; set; }
        public List<AccountDTO> Members { get; set; }
    }
}
