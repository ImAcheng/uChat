namespace uChat_API.DbModel {
    public class StaffAccount {
        public Guid StaffAccountId { get; set; }
        public Guid AccountId { get; set; }
        public byte PermissionLevel { get; set; }
        public DateTimeOffset CreateDateTime { get; set; }
        public DateTimeOffset UpdateDateTime { get; set; }

        // foreign key
        public virtual Account? Account { get; set; }
    }
}
