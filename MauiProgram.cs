using MauiMVVM.Service;
using MauiMVVM.View;
using MauiMVVM.ViewModel;

namespace MauiMVVM;

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
		//builder.Services.AddTransient<DataItemPage>();
		//builder.Services.AddTransient<DataItemDetailPage>();
		//builder.Services.AddTransient<DataItemListViewModel>();
		//builder.Services.AddTransient<DataItemDetailsViewModel>();
		//builder.Services.AddSingleton<DataItemService>();
		return builder.Build();
	}
}
