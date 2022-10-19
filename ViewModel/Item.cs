using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.ViewModel
{
    public class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Item()
        {
            value = false;
        }

        private string key;
        public string Key
        {
            get => key;
            set
            {
                if (key == value)
                    return;
                key = value;
                OnPropertyChanged();
            }
        }

        private bool value;
        public bool Value
        {
            get => value;
            set
            {
                if (this.value == value)
                    return;
                this.value = value;
                OnPropertyChanged();
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (isExpanded == value)
                    return;
                isExpanded = value;
                OnPropertyChanged();
            }
        }
    }
}
