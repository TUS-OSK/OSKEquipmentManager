using System;

using OSKEquipmentManager.ViewModels;

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
    }
}
