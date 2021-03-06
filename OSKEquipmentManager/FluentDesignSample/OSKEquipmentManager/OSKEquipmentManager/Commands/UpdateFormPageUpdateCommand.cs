﻿using OSKEquipmentManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace OSKEquipmentManager.Commands
{
    class UpdateFormPageUpdateCommand : ICommand
    {
        private UpdateFormViewModel vm;

        internal UpdateFormPageUpdateCommand(UpdateFormViewModel viewmodel)
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
            MessageDialog UpdateMessage = new MessageDialog("Updated EquipmentList!");
            UpdateMessage.Commands.Add(new UICommand("OK"));
            await UpdateMessage.ShowAsync();
        }
    }
}
