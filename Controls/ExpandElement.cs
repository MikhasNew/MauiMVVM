
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;
using MauiMVVM.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MauiMVVM.Controls
{
    internal class ExpandElement : ContentView
    {

        GroupItem groupItem = new GroupItem();

        #region GroupExpandItem
        public static readonly BindableProperty GroupExpandItemProperty = BindableProperty.Create(nameof(GroupExpandItem),
          typeof(GroupItem),
          typeof(ExpandableList),
          defaultValue: new GroupItem(),
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: FiltredGroupExpandItemPropertyChanged);
        private static void FiltredGroupExpandItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var control = (ExpandElement)bindable;
            control.groupItem = (GroupItem)newValue;
            control.Initialize();

        }

        public GroupItem GroupExpandItem
        {
            get
            {
                return (GroupItem)base.GetValue(GroupExpandItemProperty);
            }

            set
            {
                base.SetValue(GroupExpandItemProperty, value);
            }
        }
        #endregion




        public ExpandElement()
        {
        }

        private void Initialize()
        {
            groupItem = GroupExpandItem;
            var groupNameView = new Grid
            {
                ColumnDefinitions = Columns.Define(Auto, Auto, Auto),
                ColumnSpacing = 2,
                Children =
                {
                    new CheckBox{ BindingContext = groupItem}
                    .Column(0).Row(0).Bind(nameof(Item.Value))
                    .Invoke(checbox=>checbox.CheckedChanged+=Checbox_NameChanged),
                    new Label{BindingContext = groupItem,
                        VerticalOptions = LayoutOptions.Center}
                    .Column(1).Row(0).Bind(nameof(Item.Key)),
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
            var itemListView = new CollectionView
            {
                ItemsSource = groupItem.Items,
                ItemTemplate = new DataTemplate(() =>
                {
                    HorizontalStackLayout views = new HorizontalStackLayout
                    {
                        Padding = new Thickness(20, 0, 0, 0),
                        Children =
                             {
                                new CheckBox().Column(0).Bind(nameof(Item.Value))
                                .Invoke(checbox=>checbox.CheckedChanged+=Checbox_ValueChanged),
                                new Label{ VerticalOptions = LayoutOptions.Center}
                                .Column(1).Bind(nameof(Item.Key))
                             }
                    };
                    return views;
                })
            };
            var view = new Grid
            {
                ColumnDefinitions = Columns.Define(Auto, Star),
                RowDefinitions = Rows.Define(Auto, Auto),
                BindingContext = groupItem,
                Children =
                {
                   groupNameView
                   .Row(0)
                   .TapGesture(()=>{
                        groupItem.IsExpanded=!groupItem.IsExpanded;
                    }),
                   itemListView
                   .ColumnSpan(2)
                   .Row(1)
                   .Bind(Grid.IsVisibleProperty, ".IsExpanded")
                }
            };
            Content = view;
            void Checbox_NameChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    if (!groupItem.Items.Any(x => x.Value == true))
                        groupItem.Value = false;
                    return;
                }
                if (e.Value == false)
                    foreach (var v in groupItem.Items)
                        v.Value = false;

            }
            void Checbox_ValueChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    groupItem.Value = true;
                    return;
                }
                if (!groupItem.Items.Any(x => x.Value == true))
                {
                    groupItem.Value = false;
                    return;
                }

            }
        }

    }
}