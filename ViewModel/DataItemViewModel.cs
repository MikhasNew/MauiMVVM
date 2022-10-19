using MauiMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.ViewModel
{
    public class DataItemViewModel : BaseViewModel
    {
        public DataItem DataItem { get; private set; }
        public DataItemViewModel()
        {
            DataItem = new DataItem();
        }
        public DataItemViewModel(DataItem item)
        {
            this.DataItem = item;
        }
      
        private DataItemListViewModel dataItemListViewModel;
        public DataItemListViewModel DataItemListViewModel
        {
            get => dataItemListViewModel;
            set
            {
                if (dataItemListViewModel != value)
                {
                    dataItemListViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => DataItem.Name;
            set
            {
                if (DataItem.Name == value)
                    return;
                DataItem.Name = value;
                OnPropertyChanged();

            }
        }
        public string Image
        {
            get => DataItem.Image;
            set
            {
                if (DataItem.Image == value)
                    return;
                DataItem.Image = value;
                OnPropertyChanged();

            }
        }
    }
}
