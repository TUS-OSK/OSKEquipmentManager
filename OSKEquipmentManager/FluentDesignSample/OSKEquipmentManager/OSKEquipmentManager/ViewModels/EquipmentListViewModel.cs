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
                // TODO WTS: ここは実際のデータで置き換えること
                return SampleData.GetGridSampleData();
            }
        }
    }
}
