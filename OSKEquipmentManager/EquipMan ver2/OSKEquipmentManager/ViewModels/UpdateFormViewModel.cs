using OSKEquipmentManager.Commands;
using OSKEquipmentManager.Views;
using System;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using OSKEquipmentManager.Common;
using OSKEquipmentManager.Models;
using System.Linq;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;
using OSKEquipmentManager.Views.Controls;
using Windows.UI.Xaml;

namespace OSKEquipmentManager.ViewModels
{
    public enum EquipReturnDate
    {
        Undefined,
        一日後 = 1,
        三日後 = 3,
        一週間後 = 7,
    }

    public class UpdateFormViewModel : ViewModelBase
    {
        public UpdateFormViewModel()
        {
            ListVisibility = Visibility.Visible;
            UpdateVisibility = Visibility.Collapsed;
            BorrowingVisibility = Visibility.Collapsed;
            RaisePropertyChanged(nameof(ListVisibility));
            RaisePropertyChanged(nameof(UpdateVisibility));
            RaisePropertyChanged(nameof(BorrowingVisibility));

            UpdateCommand = new DelegateCommand(param =>
            {
                using (var db = new EquipmentInformationContext())
                {
                    var update = new EquipmentInformation
                    {
                        EquipmentName = this.NewEquipName,
                        BorrowingMember = this.PersonName,
                        ReturnPlanDate = this.ReturnDate,
                        //Status=EquipmentStatus.貸出中,
                        Remarks = this.RemarkText
                    };

                    db.EqInfo.Add(update);
                    db.SaveChanges();

                    //Addを押したらTextBoxが空になる様にした
                    this.newEquipName = "";
                    this.personName = "";
                    this.remarkText = "";
                    RaisePropertyChanged(nameof(NewEquipName));
                    RaisePropertyChanged(nameof(PersonName));
                    RaisePropertyChanged(nameof(RemarkText));
                    RaisePropertyChanged(nameof(EqStatus));
                    RaisePropertyChanged(nameof(ReturnDate));

                    this.ItemSources = db.EqInfo.ToList();
                }
                //ListVisibility = Visibility.Visible;
                //UpdateVisibility = Visibility.Collapsed;
                //BorrowingVisibility = Visibility.Collapsed;
                //RaisePropertyChanged(nameof(ListVisibility));
                //RaisePropertyChanged(nameof(UpdateVisibility));
                //RaisePropertyChanged(nameof(BorrowingVisibility));
            },
                () =>
                {
                    if (NewEquipName != null)
                    {
                        return true;
                    }
                    else { return false; }
                });
        }


        private EquipReturnDate _value;
        public EquipReturnDate Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                _value = value;
                RaisePropertyChanged(nameof(Is1));
                RaisePropertyChanged(nameof(Is2));
                RaisePropertyChanged(nameof(Is3));
            }
        }

        public bool Is1
        {
            get { return Value == EquipReturnDate.一日後; }
            set
            {
                if (value)
                    Value = EquipReturnDate.一日後;
                else
                    Value = EquipReturnDate.Undefined;
            }
        }

        public bool Is2
        {
            get { return Value == EquipReturnDate.三日後; }
            set
            {
                if (value)
                    Value = EquipReturnDate.三日後;
                else
                    Value = EquipReturnDate.Undefined;
            }
        }

        public bool Is3
        {
            get { return Value == EquipReturnDate.一週間後; }
            set
            {
                if (value)
                    Value = EquipReturnDate.一週間後;
                else
                    Value = EquipReturnDate.Undefined;
            }
        }

        private string newEquipName;
        public string NewEquipName
        {
            get { return this.newEquipName; }
            set
            {
                this.newEquipName = value;
                RaisePropertyChanged(nameof(NewEquipName));
                UpdateCommand.RaiseCanExecuteChanged();
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

        private string eqStatus;
        public string EqStatus
        {
            get { return this.eqStatus; }
            set
            {
                this.eqStatus = value;
                RaisePropertyChanged(nameof(EqStatus));
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
                ListVisibility = Visibility.Visible;
                UpdateVisibility = Visibility.Collapsed;
                BorrowingVisibility = Visibility.Collapsed;
                RaisePropertyChanged(nameof(ListVisibility));
                RaisePropertyChanged(nameof(UpdateVisibility));
                RaisePropertyChanged(nameof(BorrowingVisibility));
                if (ItemSources.Count >= 1)
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
                if (ItemSources.Count >= 1)
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

      

        /// <summary>
        /// 一応取得しておく「備品を借りた日」
        /// </summary>
        public DateTime DetailEqLoanDate
        {
            get
            {
                if (ItemSources.Count >= 1)
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

        /// <summary>
        /// 詳細ページで表示する「借りた人」
        /// </summary>
        public string DetailEqBorrowMem
        {
            get
            {
                if (ItemSources.Count >= 1)
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

        /// <summary>
        /// 詳細ページで表示する「備品に対するコメント」
        /// </summary>
        public string DetailEqRemarks
        {
            get
            {
                if (ItemSources.Count >= 1)
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

        public DateTime calcReturnDate()
        {
            if (ItemSources.Count >= 1)
            {
                using (var db = new EquipmentInformationContext())
                {
                    if (SelectedIndexes == -1)
                        return DateTime.Today;

                    var detail = ItemSources[SelectedIndexes];
                    if (Is1 == true)
                    {
                        //return detail.LoanDate.AddDays((int)EquipReturnDate.一日後).ToString();
                        return DateTime.Today.AddDays((int)EquipReturnDate.一日後);
                    }
                    if (Is2 == true)
                    {
                        //return detail.LoanDate.AddDays((int)EquipReturnDate.三日後).ToString();
                        return DateTime.Today.AddDays(3);
                    }
                    if (Is3 == true)
                    {
                        //return detail.LoanDate.AddDays((int)EquipReturnDate.一週間後).ToString();
                        return DateTime.Today.AddDays((int)EquipReturnDate.一週間後);
                    }
                    else
                    {
                        //return detail.LoanDate.AddDays(0).ToString();
                        return DateTime.Today.AddDays(0);
                    }
                }
            }
            return DateTime.Today.AddDays(0);
        }
        /// <summary>
        /// ListViewページ、詳細ページで表示するための
        /// 「返却予定日」
        /// </summary>
        public DateTime ReturnDate
        {
            get
            {
                if (ItemSources.Count >= 1)
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (SelectedIndexes == -1)
                            return new DateTime { };
                        var detail = ItemSources[SelectedIndexes];
                        return detail.ReturnPlanDate;
                    }
                }
                else { return new DateTime(); }
            }
        }

        /// <summary>
        /// ListView, 詳細ページで表示するための
        /// 「最終更新日時」
        /// </summary>
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



        /// <summary>
        /// Updateフォームにおける「登録」ボタン
        /// </summary>
        public DelegateCommand UpdateCommand { get; set; }

        /// <summary>
        /// Updateフォームにおけるキャンセルボタン(クリアボタン)
        /// </summary>
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
                    this.Is1 = false;
                    this.Is2 = false;
                    this.Is3 = false;
                    RaisePropertyChanged(nameof(NewEquipName));
                    RaisePropertyChanged(nameof(PersonName));
                    RaisePropertyChanged(nameof(RemarkText));
                    RaisePropertyChanged(nameof(Is1));
                    RaisePropertyChanged(nameof(Is2));
                    RaisePropertyChanged(nameof(Is3));
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

        /// <summary>
        /// ListViewにおける「削除」ボタン
        /// </summary>
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

        /// <summary>
        /// 「借りる」フォームにおいて、フォームの記載事項を
        /// 適応するボタン
        /// </summary>
        public ICommand ApplyCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (ItemSources.Count == 0)
                        {
                            if (SelectedIndexes == 0) return;
                        }
                        if (SelectedIndexes == -1) return;

                        var equip = ItemSources[SelectedIndexes];
                        equip.BorrowingMember = this.PersonName;
                        equip.ReturnPlanDate = calcReturnDate();
                        equip.Status = EquipmentStatus.貸出中;
                        equip.Remarks = this.RemarkText;

                        db.EqInfo.Update(equip);
                        if (equip != null)
                        {
                            db.SaveChanges();
                            this.ItemSources = db.EqInfo.ToList();
                        }
                        this.PersonName = "";
                        this.Is1 = true;
                        this.Is2 = false;
                        this.Is3 = false;
                        this.RemarkText = "";
                    }
                    //BorrowButtonVisibility = Visibility.Visible;
                    //ReturnButtonVisibility = Visibility.Collapsed;
                });
            }
        }

        
        public ICommand ReturnCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    using (var db = new EquipmentInformationContext())
                    {
                        if (ItemSources.Count == 0)
                        {
                            if (SelectedIndexes == 0) return;
                        }
                        if (SelectedIndexes == -1) return;

                        var equip = ItemSources[SelectedIndexes];
                        equip.BorrowingMember = "";
                        equip.ReturnPlanDate = DateTime.Today;
                        equip.Status = EquipmentStatus.利用可能;
                        //equip.Remarks = this.RemarkText;

                        db.EqInfo.Update(equip);
                        db.SaveChanges();
                        this.ItemSources = db.EqInfo.ToList();
                    }

                    //BorrowButtonVisibility = Visibility.Collapsed;
                    //ReturnButtonVisibility = Visibility.Visible;
                });
            }
        }

        /// <summary>
        /// ListViewにおける「Addボタン」
        /// 備品の登録フォームを表示する
        /// </summary>
        public ICommand AddCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    ListVisibility = Visibility.Collapsed;
                    BorrowingVisibility = Visibility.Collapsed;
                    UpdateVisibility = Visibility.Visible;
                    RaisePropertyChanged(nameof(ListVisibility));
                    RaisePropertyChanged(nameof(BorrowingVisibility));
                    RaisePropertyChanged(nameof(UpdateVisibility));
                });
            }
        }

        /// <summary>
        /// 備品詳細ページにおける「借りるボタン」
        /// 「Borrowingフォーム」を表示する
        /// </summary>
        public ICommand BorrowCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    ListVisibility = Visibility.Collapsed;
                    BorrowingVisibility = Visibility.Visible;
                    UpdateVisibility = Visibility.Collapsed;
                    RaisePropertyChanged(nameof(ListVisibility));
                    RaisePropertyChanged(nameof(BorrowingVisibility));
                    RaisePropertyChanged(nameof(UpdateVisibility));
                });
            }
        }

        public Visibility ListVisibility { get; private set; }
        public Visibility UpdateVisibility { get; private set; }
        public Visibility BorrowingVisibility { get; private set; }
        public Visibility BorrowButtonVisibility { get; set; }
        public Visibility ReturnButtonVisibility { get; set; }
    }
}
