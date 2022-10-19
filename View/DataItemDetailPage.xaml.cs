using MauiMVVM.Model;
using MauiMVVM.ViewModel;

namespace MauiMVVM.View;

public partial class DataItemDetailPage : ContentPage
{
    public DataItemViewModel ViewModel { get; private set; }
    public DataItemDetailPage(DataItemViewModel vm)
	{
		InitializeComponent();
		ViewModel = vm;
		BindingContext = ViewModel;
	}
}