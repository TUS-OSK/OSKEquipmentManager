using OSKEquipmentManager.Commands;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using OSKEquipmentManager.Common;
using OSKEquipmentManager.Models;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

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
                RaisePropertyChanged(nameof(NewEquipName));
            }
        }

        private string personName;
        public string PersonName
        {
            get { return this.personName; }
            set
            {
                this.personName = value;
                RaisePropertyChanged(nameof(PersonName));
            }
        }

        private string remarkText;
        public string RemarkText
        {
            get { return this.remarkText; }
            set
            {
                this.remarkText = value;
                RaisePropertyChanged(nameof(RemarkText));
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


        public string DetailEqNameText
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return "";
                    var detail = ItemSources[SelectedIndexes];
                    return detail.EquipmentName;
                }
            }
        }

        public EquipmentStatus DetailEqStatusText
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return EquipmentStatus.利用可能;
                    var detail = ItemSources[SelectedIndexes];
                    return detail.Status;
                }
            }
        }

        public DateTime DetailEqLoanDate
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return new DateTime { };
                     var detail = ItemSources[SelectedIndexes];
                    return detail.LoanDate;
                }
            }
        }

        public string DetailEqBorrowMem
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return "";
                    var detail = ItemSources[SelectedIndexes];
                    return detail.BorrowingMember;
                }
            }
        }

        public string DetailEqRemarks
        {
            get
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return "";
                    var detail = ItemSources[SelectedIndexes];
                    return detail.Remarks;
                }
            }
        }
        //EquipmentListViewModel ev = new EquipmentListViewModel();


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
                            EquipmentName = this.NewEquipName,
                            BorrowingMember = this.PersonName,
                            Remarks = this.RemarkText
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

        private int selectedIndexes;
        public int SelectedIndexes
        {
            get
            { return this.selectedIndexes; }
            set
            {
                this.selectedIndexes = value;
                RaisePropertyChanged(nameof(SelectedIndexes));
                RaisePropertyChanged(nameof(DetailEqNameText));
                RaisePropertyChanged(nameof(DetailEqStatusText));
                RaisePropertyChanged(nameof(DetailEqLoanDate));
                RaisePropertyChanged(nameof(DetailEqBorrowMem));
                RaisePropertyChanged(nameof(DetailEqRemarks));
            }
        }

        public ICommand DeleteCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        var itmssource = ItemSources as List<EquipmentInformation>;
                        if (SelectedIndexes == -1) return;
                        var equip = itmssource[SelectedIndexes];

                        if (equip != null)
                        {
                            db.Informations.Remove(equip);
                            db.SaveChanges();

                            this.ItemSources = db.Informations.ToList();
                        }
                    }
                });
                //public UpdateFormViewModel()
                //{
                //    UpdateCommand = new UpdateFormPageUpdateCommand(this);
                //    CancelCommand = new UpdateFormPageCancelCommand(this);
                //}
            }
        }
    }
}
