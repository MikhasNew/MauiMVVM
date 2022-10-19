namespace MauiMVVM.View;

public partial class AppFlyoutPage : FlyoutPage
{
	public AppFlyoutPage()
	{
		InitializeComponent();

		Flyout = new FlyoutContentPage();

        Detail = new AppTabbedPage();
	}
}