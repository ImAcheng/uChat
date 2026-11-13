namespace uChat_Desktop.DataModel {
    public class GeneralChatRequest {
        public Guid Requester { get; set; }
        public Guid Target { get; set; }
    }

    public class ChatDisplayDTO {
        public Guid ChatId { get; set; }
        public Guid ChatMemberId { get; set; }
        public string? ChatName { get; set; }
    }

    public class ChatDTO {
        public Guid ChatId { get; set; }
        public Guid ChatMemberId { get; set; }
        public string? ChatName { get; set; }
        public byte ChatType { get; set; }
        public List<AccountDTO> Members { get; set; }
    }

    public class GeneralChatResult {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ChatDTO? Chat { get; set; }
    }

    public class GetChatResult {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ChatDisplayDTO>? Chat { get; set; }
    }

    public class MessageDTO {
        public long MessageNo { get; set; }
        public Guid ChatId { get; set; }
        public string SenderName { get; set; }
        public string? TextContent { get; set; }
    }

    public class MessageResult {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<MessageDTO>? MsgData { get; set; }
    }
}
