using System.ComponentModel.DataAnnotations.Schema;

namespace uChat_API.DbModel {
    public class ChatMessage {
        public Guid ChatMessageId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ChatMessageNo { get; set; }

        /// <summary>
        /// 0: One-to-One Chat
        /// 1: Normal Group Chat
        /// 2: Server-Affiliated Chat
        /// </summary>
        public byte AffiliatedType { get; set; }

        public Guid ChatRoomId { get; set; }

        public Guid? ServerGroupId { get; set; }

        public Guid? ServerMemberId { get; set; }

        public Guid? ChatRoomMemberId { get; set; }

        public string? TextContent { get; set; }

        /// <summary>
        /// 0: Normal
        /// 1: Edited
        /// 2: Removed
        /// </summary>
        public byte ModifyStatus { get; set; } = 0;

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }
    }

    public class MessageDTO {
        public long MessageNo { get; set; }
        public Guid ChatId { get; set; }
        public string SenderName { get; set; }
        public string? TextContent { get; set; }

        public MessageDTO(ChatMessage msg, string sender) {
            MessageNo = msg.ChatMessageNo;
            ChatId = msg.ChatRoomId;
            TextContent = msg.TextContent;
        }
    }
}
