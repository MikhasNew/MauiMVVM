using MauiMVVM.View;

namespace MauiMVVM;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
		//MainPage = new NavigationPage( new DataItemListPage());
		//MainPage = new AppTabbedPage();
		MainPage = new AppFlyoutPage();

    }
}
