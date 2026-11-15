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

    public class MessageAttachmentDTO {
        public Guid FileDataId { get; set; }
        public byte FileType { get; set; }
        public byte[]? Thumbnail { get; set; }

        // if the link wasn't generated, then redirect user to rick roll anyways
        public string DownloadLink { get; set; } = "https://www.youtube.com/watch?v=dQw4w9WgXcQ";
    }
}
