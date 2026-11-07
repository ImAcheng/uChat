namespace uChat_API.DbModel {
    public class ChatMessageAttachment {
        public Guid ChatMessageAttachmentId { get; set; }

        public Guid FileDataId { get; set; }

        public Guid ChatMessageId { get; set; }

        /// <summary>
        /// 0: Normal
        /// 1: Removed
        /// </summary>
        public byte ModifyStatus { get; set; } = 0;

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }
    }
}
