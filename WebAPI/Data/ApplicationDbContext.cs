using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Xe> xes { get; set; }
        public DbSet<TaiKhoan> taiKhoans { get; set; }
        public DbSet<PhieuTiepNhan> phieuTiepNhans { get; set; }
        public DbSet<ThamSo> thamSos { get; set; }
        public DbSet<HieuXe> hieuXes { get; set; }
        public DbSet<VatTuPhuTung> vatTuPhuTungs { get; set; }
        public DbSet<ChiTietPhieuSuaChua> chiTietPhieuSuaChuas { get; set; }
        public DbSet<PhieuSuaChua> phieuSuaChuas { get; set; }
        public DbSet<TienCong> tienCongs { get; set; }
        public DbSet<NhomNguoiDung> nhomNguoiDungs { get; set; }
        public DbSet<PhieuThuTien> phieuThuTiens { get; set; }
        public DbSet<PhanQuyen> phanQuyens { get; set; }
        public DbSet<ChucNang> chucNangs { get; set; }
        public DbSet<KhachHang> khachHangs { get; set; }
        public DbSet<NhanVien> nhanViens { get; set; }
        public DbSet<PhieuNhapVatTu> phieuNhapVatTus { get; set; }
        public DbSet<ChiTietPhieuNhapVatTu> chiTietPhieuNhapVatTus { get; set; }
        public DbSet<BaoCaoDoanhThuThang> baoCaoDoanhThuThangs { get; set; }
        public DbSet<ChiTietBaoCaoDoanhThuThang> chiTietBaoCaoDoanhThuThangs { get; set; }
        public DbSet<BaoCaoTon> baoCaoTons { get; set; }
        public DbSet<ChiTietBaoCaoTon> chiTietBaoCaoTons { get; set; }
        public DbSet<NoiDungSuaChua> noiDungSuaChuas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tạo GUID cho các nhóm người dùng
            var adminGroupId = Guid.NewGuid();
            var userGroupId = Guid.NewGuid();

            // Seed bảng NhomNguoiDung
            modelBuilder.Entity<NhomNguoiDung>().HasData(
                new NhomNguoiDung
                {
                    Id = adminGroupId,
                    TenNhom = "Admin"
                },
                new NhomNguoiDung
                {
                    Id = userGroupId,
                    TenNhom = "User"
                }
            );

            // Seed bảng TaiKhoan
            modelBuilder.Entity<TaiKhoan>().HasData(
                new TaiKhoan
                {
                    Id = Guid.NewGuid(),
                    TenDangNhap = "admin",
                    MatKhau = "admin123", // Mật khẩu nên mã hóa bằng cách sử dụng Hash (chỉ là ví dụ)
                    NhomNguoiDungId = adminGroupId
                },
                new TaiKhoan
                {
                    Id = Guid.NewGuid(),
                    TenDangNhap = "user",
                    MatKhau = "user123", // Mật khẩu nên mã hóa bằng cách sử dụng Hash (chỉ là ví dụ)
                    NhomNguoiDungId = userGroupId
                }
            );
        }
    }
}
