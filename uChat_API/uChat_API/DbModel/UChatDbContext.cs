using Microsoft.EntityFrameworkCore;

namespace uChat_API.DbModel {
    public class UChatDbContext : DbContext {
        public UChatDbContext(DbContextOptions<UChatDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<StaffAccount> StaffAccount { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<ServerGroup> ServerGroup { get; set; }
        public DbSet<ChatRoom> ChatRoom { get; set; }
        public DbSet<ChatRoomMember> ChatRoomMember { get; set; }
        public DbSet<ServerMember> ServerMember { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }
        public DbSet<ChatMessageAttachment> ChatMessageAttachment { get; set; }
        public DbSet<Friend> Friend { get; set; }
    }
}
