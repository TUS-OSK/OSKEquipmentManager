using OSKEquipmentManager.Commands;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using OSKEquipmentManager.Common;
using OSKEquipmentManager.Models;
using System.Linq;
using System.Collections.Generic;

namespace OSKEquipmentManager.ViewModels
{
    public class UpdateFormViewModel : ViewModelBase
    {
        private string newEquipName;
        public string NewEquipName
        {
            get { return this.newEquipName; }
            set
            {
                this.newEquipName = value;
                RaisePropertyChanged(nameof(newEquipName));
            }
        }

        private string personName;
        public string PersonName
        {
            get { return this.personName; }
            set
            {
                this.personName = value;
                RaisePropertyChanged(nameof(personName));
            }
        }

        private string remarkText;
        public string RemarkText
        {
            get { return this.remarkText; }
            set
            {
                this.remarkText = value;
                RaisePropertyChanged(nameof(remarkText));
            }
        }

        private List<EquipmentInformation> itemSources;
        public List<EquipmentInformation> ItemSources
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    var souce = db.Informations.ToList();
                    return souce;
                }
            }
            set
            {
                this.itemSources = value;
                RaisePropertyChanged(nameof(ItemSources));
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        var update = new EquipmentInformation
                        {
                            EquipmentName = this.newEquipName,
                            BorrowingMember = this.personName,
                            Remarks = this.remarkText
                        };
                        db.Informations.Add(update);
                        db.SaveChanges();

                        //Addを押したらTextBoxが空になる様にした
                        this.newEquipName = "";
                        this.personName = "";
                        this.remarkText = "";
                        RaisePropertyChanged(nameof(NewEquipName));
                        RaisePropertyChanged(nameof(PersonName));
                        RaisePropertyChanged(nameof(RemarkText));

                        this.ItemSources = db.Informations.ToList();
                    }
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    //Cancelを押したらTextBoxが空になる様にした
                    this.newEquipName = "";
                    this.personName = "";
                    this.remarkText = "";
                    RaisePropertyChanged(nameof(NewEquipName));
                    RaisePropertyChanged(nameof(PersonName));
                    RaisePropertyChanged(nameof(RemarkText));
                });
            }
        }
        //public UpdateFormViewModel()
        //{
        //    UpdateCommand = new UpdateFormPageUpdateCommand(this);
        //    CancelCommand = new UpdateFormPageCancelCommand(this);
        //}
    }
}
