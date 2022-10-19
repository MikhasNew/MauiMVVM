using MauiMVVM.ViewModel;
using MauiMVVM.Service;

namespace MauiMVVM;

public partial class DataItemListPage : ContentPage
{
	public DataItemListPage()
	{
        DataItemService dataItemService = new DataItemService();
        InitializeComponent();
		BindingContext = new DataItemListViewModel()
		{
			Navigation = this.Navigation,
			DataItemService = dataItemService
		};
	}
}

