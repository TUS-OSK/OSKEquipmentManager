using System;

using OSKEquipmentManager.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace OSKEquipmentManager.Views
{
    public sealed partial class NavigationViewPage : Page
    {
        private NavigationViewModel ViewModel
        {
            get { return DataContext as NavigationViewModel; }
        }

        public NavigationViewPage()
        {
            // We use NavigationCacheMode.Required to keep track the selected item on navigation. For further information see the following links.
            // https://msdn.microsoft.com/en-us/library/windows/apps/xaml/windows.ui.xaml.controls.page.navigationcachemode.aspx
            // https://msdn.microsoft.com/en-us/library/windows/apps/xaml/Hh771188.aspx
            NavigationCacheMode = NavigationCacheMode.Required;
            InitializeComponent();

            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = false;
            ApplicationViewTitleBar titlebar = ApplicationView.GetForCurrentView().TitleBar;
            titlebar.ButtonBackgroundColor = Colors.Transparent;
            titlebar.ButtonInactiveBackgroundColor = Colors.Transparent;
        }

        private void NaviView_Loaded(object sender, RoutedEventArgs e)
        {
            NaviView.MenuItems.Add(new NavigationViewItemSeparator());

            foreach (NavigationViewItemBase item in NaviView.MenuItems)
            {
                if (item is NavigationViewItem && item.Tag.ToString() == "main") 
                NaviView.SelectedItem = item;
                break;
            }
        }

        private void NaviView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                switch (args.InvokedItem)
                {
                    case "Main":
                        ContentFrame.Navigate(typeof(MainPage));
                        break;
                    case "Add":
                        ContentFrame.Navigate(typeof(UpdateFormPage));
                        break;
                }
            }
        }

        private void NaviView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                ContentFrame.Navigate(typeof(SettingPage));
            }
            else
            {
                NavigationViewItem item = args.SelectedItem as NavigationViewItem;

                switch (item.Tag)
                {
                    case "main":
                        ContentFrame.Navigate(typeof(MainPage));
                        break;
                    case "add":
                        ContentFrame.Navigate(typeof(UpdateFormPage));
                        break;
                }
            }
        }
    }
}

