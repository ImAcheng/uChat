namespace uChat_API.DbModel {
    public class FileData {
        public Guid FileDataId { get; set; }

        public byte FileType { get; set; }

        public string FileName { get; set; } = "untitled";

        public string? FileExtention { get; set; }

        public string? MimeType { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }
    }
}
