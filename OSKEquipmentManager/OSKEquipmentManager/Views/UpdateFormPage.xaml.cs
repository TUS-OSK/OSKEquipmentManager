using System;

using OSKEquipmentManager.ViewModels;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using OSKEquipmentManager.Models;

namespace OSKEquipmentManager.Views
{
    public sealed partial class UpdateFormPage : Page
    {
        private UpdateFormViewModel ViewModel
        {
            get { return DataContext as UpdateFormViewModel; }
        }

        public UpdateFormPage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EquipmentInformationContext())
            {
                //Blogs.ItemsSource = db.Blogs.ToList();
            }
        }
    }
}
