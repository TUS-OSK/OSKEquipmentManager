using System;

using OSKEquipmentManager.ViewModels;

using Windows.UI.Xaml.Controls;

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
    }
}
