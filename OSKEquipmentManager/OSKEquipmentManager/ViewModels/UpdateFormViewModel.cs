using OSKEquipmentManager.Commands;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;

namespace OSKEquipmentManager.ViewModels
{
    public class UpdateFormViewModel : ViewModelBase
    {
        public ICommand UpdateCommand { get; set; }

        public ICommand CancelCommand { get; set; }

        public UpdateFormViewModel()
        {
            UpdateCommand = new UpdateFormPageUpdateCommand(this);
            CancelCommand = new UpdateFormPageCancelCommand(this);
        }
    }
}
