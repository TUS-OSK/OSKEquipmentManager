using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;

using OSKEquipmentManager.Models;
using OSKEquipmentManager.Services;

namespace OSKEquipmentManager.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase
    {
        public ObservableCollection<EquipmentInformation> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleData.GetGridSampleData();
            }
        }
    }
}
