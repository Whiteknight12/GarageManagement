using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<CarRecord> CarRecords { get; set; }
        public DbSet<RuleVariable> ruleVariables { get; set; }
        public DbSet<HieuXe> hieuXes { get; set; }
        public DbSet<VatTuPhuTung> vatTuPhuTungs { get; set; }
        public DbSet<NoiDungPhieuSuaChua> noiDungPhieuSuaChuas { get; set; }
        public DbSet<PhieuSuaChua> phieuSuaChuas { get; set; }
        public DbSet<TienCong> tienCongs { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<PhieuThuTien> phieuThuTiens { get; set; }
    }
}
