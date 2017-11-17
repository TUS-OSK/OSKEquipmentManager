﻿using OSKEquipmentManager.Commands;
using OSKEquipmentManager.Views;
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
    public enum EquipReturnDate
    {
        Undefined,
        一日後 = 1,
        三日後 = 3,
        一週間後 = 7,
    }

    public class UpdateFormViewModel : ViewModelBase
    {
        private EquipReturnDate _value;
        public EquipReturnDate Value
        {
            get { return _value; }
            set
            {
                if (_value == value)
                    return;
                if (!Enum.IsDefined(typeof(EquipReturnDate), value))
                    throw new ArgumentOutOfRangeException();
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

        /// <summary>
        /// ListViewページ、詳細ページで表示するための
        /// 「返却予定日」
        /// </summary>
        public string ReturnDate
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
                        return detail.LoanDate.AddDays((int)EquipReturnDate.一日後).ToString();
                    }
                }
                else { return "-"; }
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
                            ReturnPlanDate = DateTime.Parse(this.ReturnDate),
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


                        this.ItemSources = db.EqInfo.ToList();
                    }
                });
            }
        }

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
        /// 編集フォームにおける「適用」ボタン
        /// </summary>
        public ICommand EditApplyCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {

                });
            }
        }

    　public ICommand AddCommand
        {
            get
            {
                return new DelegateCommand(param =>
                {
                    MainPage main = new MainPage();
                    
                    main.EquipMan.Navigate(typeof(UpdateFormPage));
                });
            }
        }
    }
}
