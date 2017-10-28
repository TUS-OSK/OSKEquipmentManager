using System;
using System.Collections.ObjectModel;

using GalaSoft.MvvmLight;
using OSKEquipmentManager.Views;
using OSKEquipmentManager.Models;
using OSKEquipmentManager.Services;
using System.Collections.Generic;
using System.Linq;
using OSKEquipmentManager.Common;

namespace OSKEquipmentManager.ViewModels
{
    public class EquipmentListViewModel : ViewModelBase
    {
        //public ObservableCollection<EquipmentInformation> Source
        //{
        //    get
        //    {
        //        // TODO WTS: ここは実際のデータで置き換えること
        //        return SampleData.GetGridSampleData();
        //    }
        //}

        //public EquipmentListViewModel()
        //{
        //    using(var db=new EquipmentInformationContext())
        //    {
        //        db.Informations.ToList();
        //        RaisePropertyChanged(nameof(ItemSources));
        //    }
        //}        
        

        //private List<EquipmentInformation> itemSources;
        //public List<EquipmentInformation> ItemSources
        //{
        //    get
        //    {
        //        using (var db = new EquipmentInformationContext())
        //        {
        //            var souce = db.Informations.ToList();
        //            return souce;
        //        }
        //    }
        //    set
        //    {
        //        this.itemSources = value;
        //        RaisePropertyChanged(nameof(ItemSources));
        //    }
        //}
    }
}
