using System;

using OSKEquipmentManager.ViewModels;
using OSKEquipmentManager.Views.Controls;

using Windows.UI.Xaml.Controls;

namespace OSKEquipmentManager.Views
{
    public sealed partial class MainPage : Page
    {
        private MainViewModel ViewModel
        {
            get { return DataContext as MainViewModel; }
        }

        public MainPage()
        {
            InitializeComponent();
        }

        private void EquipmentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void EquipListViewControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private void EquipListViewControl_Loaded_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
