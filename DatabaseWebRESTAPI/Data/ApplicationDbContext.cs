using DatabaseWebRESTAPI.Domain.Entities;
using DatabaseWebRESTAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace DatabaseWebRESTAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            _ = modelBuilder.Entity<UserAccount>().HasData([
                new UserAccount
                {
                    UserAccountID=1,
                    Username="admin1",
                    Password="$2a$11$TDDB6C/PpAeHNQKTO4p6qOmZw9B7GZX1g.fmFqZmr4YVGVhJZ5FSO",
                    Role="Admin"
                }
            ]);
        }
    }
}
