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

        private DateTime returnPlanDay;
        public DateTime ReturnPlanDay
        {
            get { return this.returnPlanDay; }
            set
            {
                this.returnPlanDay = value;
                RaisePropertyChanged(nameof(ReturnPlanDay));
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
                    var souce = db.EqInfo.ToList();
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
                if (ItemSources.Count>=1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return "";
                        var detail = ItemSources[SelectedIndexes];
                        return detail.EquipmentName;
                    }

                }
                else { return ""; }
            }
        }

        public EquipmentStatus DetailEqStatusText
        {
            get
            {
                if (ItemSources.Count>=1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return EquipmentStatus.利用可能;
                        var detail = ItemSources[SelectedIndexes];
                        return detail.Status;
                    }
                }
                else { return EquipmentStatus.利用可能; }
            }
        }

        public DateTime DetailEqLoanDate
        {
            get
            {
                if (ItemSources.Count>=1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return new DateTime { };
                        var detail = ItemSources[SelectedIndexes];
                        return detail.LoanDate;
                    }
                }
                else { return DateTime.Today; }
            }
        }

        public string DetailEqBorrowMem
        {
            get
            {
                if (ItemSources.Count>=1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return "";
                        var detail = ItemSources[SelectedIndexes];
                        return detail.BorrowingMember;
                    }
            }
                else { return ""; }
        }
        }

        public string DetailEqRemarks
        {
            get
            {
                if (ItemSources.Count>=1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return "";
                        var detail = ItemSources[SelectedIndexes];
                        return detail.Remarks;
                    }
                }
                else { return ""; }
            }
        }

        public string LastUpDate
        {
            get
            {
                if (ItemSources.Count >= 1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return "-";
                        var detail = ItemSources[SelectedIndexes];
                        return detail.LastUpdateDate.ToString();
                    }
                }
                else { return "-"; }
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
                            EquipmentName = this.NewEquipName,
                            BorrowingMember = this.PersonName,
                            ReturnPlanDate=this.ReturnPlanDay, //
                            Remarks = this.RemarkText
                        };

                        db.EqInfo.Add(update);
                        db.SaveChanges();

                        //Addを押したらTextBoxが空になる様にした
                        this.newEquipName = "";
                        this.personName = "";
                        this.returnPlanDay = new DateTime().AddDays(14);
                        this.remarkText = "";
                        RaisePropertyChanged(nameof(NewEquipName));
                        RaisePropertyChanged(nameof(PersonName));
                        RaisePropertyChanged(nameof(ReturnPlanDay));
                        RaisePropertyChanged(nameof(RemarkText));


                        this.ItemSources = db.EqInfo.ToList();
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
                    this.returnPlanDay= new DateTime().AddDays(14);
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

                        if (ItemSources.Count == 0)
                        {
                            if (SelectedIndexes == 0) return;
                        }
                        if (SelectedIndexes == -1) return;

                        var equip = itmssource[SelectedIndexes];

                        if (equip != null)
                        {
                            db.EqInfo.Remove(equip);
                            db.SaveChanges();

                            this.ItemSources = db.EqInfo.ToList();
                        }
                    }
                });
            }
        }


        public ICommand EditCancelCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {

                });
            }
        }

        public ICommand EditApplyCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {

                });
            }
        }
    }
}
