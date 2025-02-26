using DatabaseWebRESTAPI.Domain.Entities;
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
    }
}
