using APIClient.IoC;
using MAUIProject.Models;
using MAUIProject.Pages;
using MAUIProject.Services;
using MAUIProject.ViewModels;
using Microsoft.Extensions.Logging;

namespace MAUIProject;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});
        builder.Services.AddAPIClientService<Car>(x => x.APIBaseAddress = "http://10.0.2.2:5015", "/api/Car");
        builder.Services.AddAPIClientService<User>(x => x.APIBaseAddress = "http://10.0.2.2:5015", "/api/User");
		builder.Services.AddLoginService(x => x.APIBaseAddress = "http://10.0.2.2:5015", "/api/Account");


        builder.Services.AddScoped<UserViewModel>();
		builder.Services.AddScoped<AuthenticationService>();

        builder.Services.AddScoped<LoginPage>();
		builder.Services.AddScoped<CreateAccountPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
