using OSKEquipmentManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace OSKEquipmentManager.Commands
{
    class UpdateFormPageCancelCommand : ICommand
    {
        private UpdateFormViewModel vm;

        internal UpdateFormPageCancelCommand(UpdateFormViewModel viewmodel)
        {
            vm = viewmodel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public async void Execute(object parameter)
        {
            MessageDialog CancelMessage = new MessageDialog("Do you quit?");
            CancelMessage.Commands.Add(new UICommand("Continue Edit"));
            CancelMessage.Commands.Add(new UICommand("Quit"));
            await CancelMessage.ShowAsync();
        }
    }
}
