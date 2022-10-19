using MauiMVVM.Controls;
using MauiMVVM.Service;
using MauiMVVM.View;
using System.Collections.ObjectModel;


namespace MauiMVVM.ViewModel
{
    public class DataItemListViewModel : BaseViewModel
    {

        public DataItemService DataItemService { get; set; }

        private List<DataItemViewModel> dataItems { get; set; } = new();
        public ObservableCollection<DataItemViewModel> DataItems { get; set; }

        public INavigation Navigation { get; set; }
        public Command GetDataItemsComand { get; }
        public Command GetDataItemsDetailPageComand { get; }


        string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText == value)
                    return;
                searchText = value;
                if (string.IsNullOrWhiteSpace(searchText))
                    SearchDataItems();
                // OnPropertyChanged(); //если текст не меняется из кода применять нет смысла

            }
        }

        DataItemViewModel selectedDataItem;
        public DataItemViewModel SelectedDataItem
        {
            get => selectedDataItem;
            set
            {
                if (selectedDataItem == value)
                    return;
                selectedDataItem = value;
            }
        }

        public DataItemListViewModel()
        {
            DataItems = new ObservableCollection<DataItemViewModel>();
            GetDataItemsComand = new Command(async () => await GetDataItemAsync());
            SearchDataItemsComand = new Command(SearchDataItems);
            GetDataItemsDetailPageComand = new Command(GetDataItemsDetailPage);

            //var filtredFilds = new Dictionary<string, string[]>();
            //filtredFilds.Add("test", new string[] { "123", "321", "123", "321" });
            //filtredFilds.Add("test2", new string[] { "123", "321", "123", "321" });
            //filtredFilds.Add("test3", new string[] { "123", "321", "123", "321" });
          //  FiltredFilds = filtredFilds;
        }

        async void GetDataItemsDetailPage(object obj)
        {
            await Navigation.PushAsync(new DataItemDetailPage((obj as DataItemViewModel)));
        }

        async Task GetDataItemAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if (dataItems.Count != 0)
                    dataItems.Clear();
                var dataItemsFromDb = await DataItemService.GetDataItems();
                for (int i = 0; i < 10; i++)
                {
                    foreach (var data in dataItemsFromDb)
                    {
                        dataItems.Add(new DataItemViewModel(data)
                        {
                            DataItemListViewModel = this
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get DataItems: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to get DataItems: {ex.Message}", "Ok");
            }
            finally
            {
                foreach (var data in dataItems)
                {
                    DataItems.Add(data);

                }
                IsBusy = false;
                InitializeFilter();
            }
            
        }



        private List<GroupItem> filtredFilds=new();
        public List<GroupItem> FiltredFilds
        {
            get => filtredFilds;
            set
            {
                if (filtredFilds == value)
                    return;
                filtredFilds = value;
                OnPropertyChanged(); //если текст не меняется из кода применять нет смысла
            }
        }

        public Command SearchDataItemsComand { get; }
        void SearchDataItems()
        {
            if (string.IsNullOrEmpty(searchText))
            {
                DataItems.Clear();
                dataItems.ForEach(d => DataItems.Add(d));
            }
            else
            {
                DataItems.Clear();
                var result = dataItems.Where(d => d.Name.Contains(searchText)).ToList();
                result.ForEach(d => DataItems.Add(d));
            }
        }
        void InitializeFilter()
        {
            var values = new List<Item>();
            dataItems.GroupBy(x=>x.Name).Select(x=>x.First()).ToList()
                .ForEach(x => values.Add(new Item { Key = x.Name }));
            var dFiltredFilds = new List<GroupItem>();
            dFiltredFilds.Add( new() { Key= "Name", Items= values.ToArray() });
            dFiltredFilds.Add( new() { Key= "Name2", Items= values.ToArray() });
            FiltredFilds = dFiltredFilds;
        }

    }
}
