using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui;
using GarageManagement.Pages;
using GarageManagement.Services;
using GarageManagement.ViewModels;
using Microsoft.Extensions.Logging;

namespace GarageManagement;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		string baseaddress;
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<MainPage>();
#if ANDROID
		baseaddress= "https://10.0.2.2:7228/";
#elif WINDOWS
		baseaddress= "https://localhost:7228/";
#else
    baseaddress = "http://192.168.1.100:5142/"; 
#endif
        builder.Services.AddAPIClientService<Xe>(x => x.BaseAddress = baseaddress, $"{nameof(Xe)}");
        builder.Services.AddAPIClientService<PhieuTiepNhan>(x => x.BaseAddress = baseaddress, $"{nameof(PhieuTiepNhan)}");
        builder.Services.AddAPIClientService<ThamSo>(x => x.BaseAddress = baseaddress, $"{nameof(ThamSo)}");
        builder.Services.AddAPIClientService<HieuXe>(x => x.BaseAddress = baseaddress, $"{nameof(HieuXe)}");
        builder.Services.AddAPIClientService<TienCong>(x => x.BaseAddress = baseaddress, $"{nameof(TienCong)}");
        builder.Services.AddAPIClientService<PhieuSuaChua>(x => x.BaseAddress = baseaddress, $"{nameof(PhieuSuaChua)}");
        builder.Services.AddAPIClientService<ChiTietPhieuSuaChua>(x => x.BaseAddress = baseaddress, $"{nameof(ChiTietPhieuSuaChua)}");
        builder.Services.AddAPIClientService<VatTuPhuTung>(x => x.BaseAddress = baseaddress, $"{nameof(VatTuPhuTung)}");
        builder.Services.AddAPIClientService<KhachHang>(x => x.BaseAddress = baseaddress, $"{nameof(KhachHang)}");
        builder.Services.AddAPIClientService<PhieuThuTien>(x => x.BaseAddress = baseaddress, $"{nameof(PhieuThuTien)}");
        builder.Services.AddAPIClientService<PhanQuyen>(x => x.BaseAddress = baseaddress, $"{nameof(PhanQuyen)}");
        builder.Services.AddAPIClientService<NhomNguoiDung>(x => x.BaseAddress = baseaddress, $"{nameof(NhomNguoiDung)}");
        builder.Services.AddAPIClientService<ChucNang>(x => x.BaseAddress = baseaddress, $"{nameof(ChucNang)}");
        builder.Services.AddAPIClientService<TaiKhoan>(x => x.BaseAddress = baseaddress, $"{nameof(TaiKhoan)}");
		builder.Services.AddAPIClientService<NhanVien>(x => x.BaseAddress = baseaddress, $"{nameof(NhanVien)}");
        builder.Services.AddAPIClientService<PhieuNhapVatTu>(x => x.BaseAddress = baseaddress, $"{nameof(PhieuNhapVatTu)}");
        builder.Services.AddAPIClientService<ChiTietPhieuNhapVatTu>(x => x.BaseAddress = baseaddress, $"{nameof(ChiTietPhieuNhapVatTu)}");
        builder.Services.AddAPIClientService<BaoCaoDoanhThuThang>(x => x.BaseAddress = baseaddress, $"{nameof(BaoCaoDoanhThuThang)}");
        builder.Services.AddAPIClientService<ChiTietBaoCaoDoanhThuThang>(x => x.BaseAddress = baseaddress, $"{nameof(ChiTietBaoCaoDoanhThuThang)}");
        builder.Services.AddAPIClientService<BaoCaoTon>(x => x.BaseAddress = baseaddress, $"{nameof(BaoCaoTon)}");
        builder.Services.AddAPIClientService<ChiTietBaoCaoTon>(x => x.BaseAddress = baseaddress, $"{nameof(ChiTietBaoCaoTon)}");
        builder.Services.AddAPIClientService<NoiDungSuaChua>(x=>x.BaseAddress=baseaddress, nameof(NoiDungSuaChua));
        builder.Services.AddAPIClientService<VTPTChiTietPhieuSuaChua>(x => x.BaseAddress = baseaddress, $"{nameof(VTPTChiTietPhieuSuaChua)}");

        builder.Services.AddScoped<AuthenticationService>(provider=>
		{
            return new AuthenticationService(baseaddress);
        });
		builder.Services.AddScoped<UniqueConstraintCheckingService>();

        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<TiepNhanXePage>();
		builder.Services.AddTransient<TaoPhieuSuaChuaPage>();
		builder.Services.AddTransient<QuanLiXePage>();
		builder.Services.AddTransient<ChiTietXePage>();
		builder.Services.AddTransient<ThuTienPage>();
		builder.Services.AddTransient<BaoCaoDoanhSoPage>();
		builder.Services.AddTransient<ThemXePage>();
        builder.Services.AddTransient<LapPhieuNhapPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<QuanLiDanhSachHieuXePage>();
        builder.Services.AddTransient<ThemHieuXePage>();
        builder.Services.AddTransient<QuanLiDanhSachLoaiVatTuPage>();
        builder.Services.AddTransient<ChiTietKhachHangPage>();
        builder.Services.AddTransient<PhanQuyenPage>();

        builder.Services.AddTransient<TaoPhieuSuaChuaPageViewModel>();
		builder.Services.AddTransient<ThuTienPageViewModel>();
		builder.Services.AddTransient<BaoCaoDoanSoPageViewModel>();
        builder.Services.AddTransient<ThemXePageViewModel>();
        builder.Services.AddTransient<QuanLiXePageViewModel>();
        builder.Services.AddTransient<LapPhieuNhapPageViewModel>();
        builder.Services.AddTransient<MainPageViewModel>();
        builder.Services.AddTransient<QuanLiDanhSachHieuXePageViewModel>();
        builder.Services.AddTransient<ThemHieuXePageViewModel>();
        builder.Services.AddTransient<TiepNhanXePageViewModel>();
        builder.Services.AddTransient<ChiTietXePageViewModel>();
        builder.Services.AddTransient<ChiTietKhachHangViewModel>();
        builder.Services.AddTransient<NhanSuMainPageViewModel>();
        builder.Services.AddTransient<PhanQuyenPageViewModel>();

        builder.Services.AddLogging(logging =>
        {
            //logging.AddConsole();
            logging.AddDebug();   // Ghi log ra debug window
            logging.SetMinimumLevel(LogLevel.Information); // Ghi tất cả log từ Information trở lên
        });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
	}
}
