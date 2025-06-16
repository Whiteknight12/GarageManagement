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
        public DbSet<VTPTChiTietPhieuSuaChua> vtptChiTietPhieuSuaChuas { get; set; }
        public DbSet<LichSu> lichSus { get; set; }
        public DbSet<ThongBao> thongBaos { get; set; }
        public DbSet<NguoiDungThongBao> nguoiDungThongBaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
