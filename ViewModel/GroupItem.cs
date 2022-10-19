using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.ViewModel
{
    public class GroupItem : Item
    {
        private Item[] items;
        public Item[] Items
        {
            get => items;
            set
            {
                if (items == value)
                    return;
                items = value;
                OnPropertyChanged();
            }
        }
    }
}