using System;

using OSKEquipmentManager.ViewModels;

using Windows.UI.Xaml.Controls;

namespace OSKEquipmentManager.Views
{
    public sealed partial class EquipmentListPage : Page
    {
        private EquipmentListViewModel ViewModel
        {
            get { return DataContext as EquipmentListViewModel; }
        }

        // TODO WTS: Change the grid as appropriate to your app.
        // For help see http://docs.telerik.com/windows-universal/controls/raddatagrid/gettingstarted
        // You may also want to extend the grid to work with the RadDataForm http://docs.telerik.com/windows-universal/controls/raddataform/dataform-gettingstarted
        public EquipmentListPage()
        {
            InitializeComponent();
        }
    }
}
