using APIClassLibrary;
using APIClassLibrary.APIModels;
using CommunityToolkit.Maui;
using GarageManagement.Pages;
using GarageManagement.Services;
using GarageManagement.ViewModels;
using Microcharts.Maui;
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
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<MainPage>();
#if ANDROID
		baseaddress= "http://10.0.2.2:5142/";
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
        builder.Services.AddAPIClientService<LichSu>(x=>x.BaseAddress = baseaddress, $"{nameof(LichSu)}");


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
        builder.Services.AddTransient<QuanLyThamSoPage>();
        builder.Services.AddTransient<QuanLiTaiKhoanPage>();
        builder.Services.AddTransient<AddNewAccountPage>();
        builder.Services.AddTransient<SuaHieuXePage>();
        builder.Services.AddTransient<QuanLiNhanVienPage>();
        builder.Services.AddTransient<QuanLiKhachHangPage>();
        builder.Services.AddTransient<QuanLiPhieuTiepNhanXePage>();
        builder.Services.AddTransient<SuaTaiKhoanPage>();
        builder.Services.AddTransient<QuanLiPhieuNhapPage>();
        builder.Services.AddTransient<QuanLiPhieuSuaChuaPage>();
        builder.Services.AddTransient<QuanLiPhieuThuTienPage>();
        builder.Services.AddTransient<ChiTietPhieuNhapVatTuPage>();
        builder.Services.AddTransient<ChiTietPhieuSuaChuaPage>();
        builder.Services.AddTransient<LoaiTienCongPage>();
        builder.Services.AddTransient<ThemLoaiTienCongPage>();
        builder.Services.AddTransient<ThemLoaiVatTuPhuTungPage>();
        builder.Services.AddTransient<ChiTietPhieuSuaChuaPage>();   
        builder.Services.AddTransient<ChiTietNhanVienPage>();
        builder.Services.AddTransient<NhanSuMainPage>();
        builder.Services.AddTransient<DanhSachXeKhachHangPage>();
        builder.Services.AddTransient<LichSuPage>();
        builder.Services.AddTransient<BaoCaoDoanhSoListPage>();
        builder.Services.AddTransient<BaoCaoTonPage>();
        builder.Services.AddTransient<SuaPhieuSuaChuaPage>();
        builder.Services.AddTransient<ChinhSuaPhieuNhapPage>();
        builder.Services.AddTransient<ChinhSuaPhieuThuTienPage>();
        builder.Services.AddTransient<ChangePasswordPage>();

        builder.Services.AddTransient<LoginPageViewModel>();
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
        builder.Services.AddTransient<QuanLyThamSoPageViewModel>();
        builder.Services.AddTransient<QuanLiTaiKhoanPageViewModel>();
        builder.Services.AddTransient<AddNewAccountPageViewModel>();
        builder.Services.AddTransient<SuaHieuXePageViewModel>();
        builder.Services.AddTransient<QuanLiNhanVienPageViewModel>();
        builder.Services.AddTransient<QuanLiKhachHangPageViewModel>();
        builder.Services.AddTransient<QuanLiPhieuTiepNhanPageViewModel>();
        builder.Services.AddTransient<SuaTaiKhoanPageViewModel>();
        builder.Services.AddTransient<QuanLiPhieuNhapPageViewModel>();
        builder.Services.AddTransient<QuanLiPhieuSuaChuaPageViewModel>();
        builder.Services.AddTransient<QuanLiPhieuThuTienPageViewModel>();
        builder.Services.AddTransient<ChiTietPhieuNhapVatTuPageViewModel>();
        builder.Services.AddTransient<ChiTietPhieuSuaChuaPageViewModel>();
        builder.Services.AddTransient<LoaiTienCongPageViewModel>();
        builder.Services.AddTransient<ThemLoaiTienCongPageViewModel>();
        builder.Services.AddTransient<ThemLoaiVatTuPhuTungPageViewModel>();
        builder.Services.AddTransient<BaoCaoDoanhSoListPageViewModel>();
        builder.Services.AddTransient<ChinhSuaPhieuNhapPageViewModel>();
        builder.Services.AddTransient<ChinhSuaPhieuThuTienPageViewModel>();
        builder.Services.AddTransient<QuanLiDanhSachLoaiVatTuPageViewModel>(provider =>
            new QuanLiDanhSachLoaiVatTuPageViewModel(
            provider.GetService<APIClientService<VatTuPhuTung>>(),
            provider.GetService<ThemLoaiVatTuPhuTungPageViewModel>()));
        builder.Services.AddTransient<ChiTietNhanVienPageViewModel>(); 
        builder.Services.AddTransient<DanhSachXeKhachHangPageViewModel>();
        builder.Services.AddTransient<LichSuPageViewModel>();
        builder.Services.AddTransient<BaoCaoTonViewModel>();
        builder.Services.AddTransient<SuaPhieuSuaChuaPageViewModel>();
        builder.Services.AddTransient<ChangePasswordPageViewmodel>();

        builder.Services.AddLogging(logging =>
        {
            //logging.AddConsole();
            logging.AddDebug();   // Ghi log ra debug window
            logging.SetMinimumLevel(LogLevel.Information); // Ghi tất cả log từ Information trở lên
        });

        builder.Services
       .Configure<SmtpOptions>(builder.Configuration.GetSection("Smtp"));
     
        builder.Services.AddSingleton<IEmailService, SmtpEmailService>();


#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
	}
}
