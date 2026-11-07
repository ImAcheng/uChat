using System.ComponentModel.DataAnnotations.Schema;

namespace uChat_API.DbModel {
    public class ServerGroup {
        public Guid ServerGroupId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ServerGroupNo { get; set; }

        public string ServerName { get; set; } = string.Empty;

        public Guid? ServerAvatarId { get; set; }

        public DateTimeOffset CreateDateTime { get; set; }

        public DateTimeOffset? UpdateDateTime { get; set; }
    }
}
