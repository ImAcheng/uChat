using Microsoft.EntityFrameworkCore;

namespace uChat_API.DbModel {
    public class UChatDbContext : DbContext {
        public UChatDbContext(DbContextOptions<UChatDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<StaffAccount> StaffAccount { get; set; }
    }
}
