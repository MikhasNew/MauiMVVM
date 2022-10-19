using MauiMVVM.Model;
using MauiMVVM.ViewModel;

namespace MauiMVVM.View;

public partial class AppTabbedPage : TabbedPage
{
    public AppTabbedPage()
    {
        InitializeComponent();
        NavigationPage navigationPage = new NavigationPage(new DataItemListPage());
        navigationPage.IconImageSource = "schedule.png";
        navigationPage.Title = "Schedule";

        NavigationPage navigationPage2 = new NavigationPage(new DataItemListPage());
        navigationPage2.IconImageSource = "schedule.png";
        navigationPage2.Title = "Schedule2";

        Children.Add(navigationPage);
        Children.Add(navigationPage2);
    }
}