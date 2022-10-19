using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using static CommunityToolkit.Maui.Markup.GridRowsColumns;


namespace MauiMVVM.Controls;

public class ExpandableList : ContentView
{
    #region BindingTest
    //public static readonly BindableProperty GroupItemPoperty = 
    //    BindableProperty.Create(nameof(GroupItem), typeof(string),
    //        typeof(ExpandableList), string.Empty,
    //        defaultBindingMode: BindingMode.OneWay,
    //        propertyChanged: GroupItemPropertyChanged);


    //////////////////public static readonly BindableProperty GroupNameProperty = BindableProperty.Create(nameof(GroupName), typeof(string),
    //////////////////    typeof(ExpandableList), default(string), propertyChanged: GroupNamePropertyChanged);
    //////////////////public string GroupName
    //////////////////{
    //////////////////    get => (string)GetValue(GroupNameProperty);
    //////////////////    set => SetValue(GroupNameProperty, value);
    //////////////////}
    //////////////////private static void GroupNamePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    //////////////////{
    //////////////////    groupItem = new Item();
    //////////////////    groupItem.Key = (string)newValue;
    //////////////////}



    //////////////////public static readonly BindableProperty GroupValuesProperty = BindableProperty.Create(nameof(GroupValues), typeof(string[]),
    //////////////////   typeof(ExpandableList), default(string), propertyChanged: GroupValuesPropertyChanged);

    //////////////////private static void GroupValuesPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    //////////////////{
    //////////////////    values = new List<Item>();
    //////////////////    for (int i = 0; i < 10; i++)
    //////////////////    {
    //////////////////        foreach (var vl in (string[])newValue)
    //////////////////            values.Add(new Item { Key = vl });
    //////////////////    }
    //////////////////}

    //////////////////public string GroupValues
    //////////////////{
    //////////////////    get => (string)GetValue(GroupValuesProperty);
    //////////////////    set => SetValue(GroupValuesProperty, value);
    //////////////////}

    #endregion

    private static List<Item> values = new List<Item>();
    private static Item groupItem = new Item();

    public string GroupItem
    {
        get => groupItem.Key;
        set
        {
            groupItem.Key = value;
        }
    }
    public List<string> Values
    {
        get
        {
            return values.Select(x => x.Key).ToList();
        }

        set
        {
            foreach (var vl in value)
                values.Add(new Item { Key = vl });
        }

    }

  
    public ExpandableList()
    {
        Initialize();
    }

    private void Initialize()
    {
        var groupNameView = new Grid
        {
            ColumnDefinitions = Columns.Define(Auto, Star, Star),
            ColumnSpacing = 2,
            Children =
            {
                new CheckBox{ BindingContext = groupItem}
                .Column(0).Row(0).Bind(".Value")
                .Invoke(checbox=>checbox.CheckedChanged+=Checbox_NameChanged),
                new Label{BindingContext = groupItem,
                    VerticalOptions = LayoutOptions.Center}
                .Column(1).Row(0).Bind(".Key"),
                new Image{Source = "expand_more.png"}
                .Size(15,25)
                 .Bind(Grid.IsVisibleProperty,
                 nameof(Item.IsExpanded),
                 converter: new InvertedBoolConverter())
                 .CenterVertical()
                .Column(2).Row(0),
                 new Image{Source = "expand_less.png"}
                .Size(15,25)
                .Bind(Grid.IsVisibleProperty, nameof(Item.IsExpanded))
                .CenterVertical()
                .Column(2).Row(0)
            }
        };
        var itemList = new CollectionView
        {
            ItemsSource = values,
            ItemTemplate = new DataTemplate(() =>
            {
                Grid views = new Grid
                {
                    Padding = new Thickness(20, 0, 0, 0),
                    ColumnDefinitions = Columns.Define(Auto, Star),
                    Children =
                    {
                        new CheckBox().Column(0).Bind(".Value")
                        //.Bind(CheckBox.IsVisibleProperty, ".IsExpanded")
                        .Invoke(checbox=>checbox.CheckedChanged+=Checbox_ValueChanged),
                        new Label{ VerticalOptions = LayoutOptions.Center}
                        .Column(1).Bind(".Key")
                        //.Bind(CheckBox.IsVisibleProperty, ".IsExpanded")
                    }
                };
                return views;
            })
        };
        var view = new Grid
        {
            ColumnDefinitions = Columns.Define(Auto, Star),
            RowDefinitions = Rows.Define(Star, Auto),
            BindingContext = groupItem,
            Children =
            {
               groupNameView
               .Row(0)
               .TapGesture(()=>{
                    groupItem.IsExpanded=!groupItem.IsExpanded;
                }),
               itemList
               .ColumnSpan(2)
               .Row(1)
               .Bind(Grid.IsVisibleProperty, ".IsExpanded")
            }
        };
        Content = view;
    }

    private void Checbox_NameChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            if (!values.Any(x => x.Value == true))
                groupItem.Value = false;
            return;
        }
        if (e.Value == false)
            foreach (var v in values)
                v.Value = false;

    }

    private void Checbox_ValueChanged(object sender, CheckedChangedEventArgs e)
    {
        if (e.Value == true)
        {
            groupItem.Value = true;
            return;
        }
        if (!values.Any(x => x.Value == true))
        {
            groupItem.Value = false;
            return;
        }

    }

    private class Item : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public Item()
        {
            //key = "Name";
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
                if (this.isExpanded == value)
                    return;
                this.isExpanded = value;
                OnPropertyChanged();
            }
        }
    }
}