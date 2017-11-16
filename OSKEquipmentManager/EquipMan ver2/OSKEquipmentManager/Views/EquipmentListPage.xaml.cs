using System;

using OSKEquipmentManager.ViewModels;
using OSKEquipmentManager.Views;
using OSKEquipmentManager.Views.Controls;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using OSKEquipmentManager.Models;
using System.Linq;

namespace OSKEquipmentManager.Views
{
    public sealed partial class EquipmentListPage : Page
    {
        private EquipmentListViewModel ViewModel
        {
            get { return DataContext as EquipmentListViewModel;  }
        }

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public EquipmentListPage()
        {
            InitializeComponent();
        }



        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new EquipmentInformationContext())
            {
                EquipmentList.ItemsSource = db.Informations.ToList();
            }
        }

        private void EquipmentList_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
