using APIClient.IoC;
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
		builder.Services.AddAPIClientService(x => x.APIBaseAddress = "http://10.0.2.2:5015/");

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
