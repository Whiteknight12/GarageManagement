using APIClassLibrary;
using APIClassLibrary.APIModels;
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
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
		builder.Services.AddTransient<MainPage>();
#if ANDROID
		baseaddress= "http://10.0.2.2:5142/";
#elif WINDOWS
		baseaddress= "http://localhost:5142/";
#else
    baseaddress = "http://192.168.1.100:5142/"; 
#endif
        builder.Services.AddAPIClientService<Car>(x => x.BaseAddress = baseaddress, "Car");
		builder.Services.AddAPIClientService<CarRecord>(x => x.BaseAddress = baseaddress, "CarRecord");
		builder.Services.AddAPIClientService<RuleVariable>(x => x.BaseAddress = baseaddress, "RuleVariable");
		builder.Services.AddAPIClientService<HieuXe>(x => x.BaseAddress = baseaddress, "HieuXe");
		builder.Services.AddAPIClientService<TienCong>(x => x.BaseAddress = baseaddress, "TienCong");
		builder.Services.AddAPIClientService<ChiTietTienCong>(x => x.BaseAddress = baseaddress, "ChiTietTienCong");
		builder.Services.AddAPIClientService<PhieuSuaChua>(x => x.BaseAddress = baseaddress, "PhieuSuaChua");
		builder.Services.AddAPIClientService<NoiDungPhieuSuaChua>(x => x.BaseAddress = baseaddress, "NoiDungPhieuSuaChua");
		builder.Services.AddAPIClientService<VatTuPhuTung>(x => x.BaseAddress = baseaddress, "VatTuPhuTung");
		builder.Services.AddScoped<AuthenticationService>(provider=>
		{
            return new AuthenticationService(baseaddress);
        });
        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<TiepNhanXePage>();
		builder.Services.AddTransient<TaoPhieuSuaChuaPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
