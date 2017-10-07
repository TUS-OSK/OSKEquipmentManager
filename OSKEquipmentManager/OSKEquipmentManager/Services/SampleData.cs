using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using OSKEquipmentManager.Models;

namespace OSKEquipmentManager.Services
{
    public static class SampleData
    {
        //とりあえず、サンプルデータのクラスをつくっておいた。
        // TODO WTS: Entity Frameworkによるデータ処理ができたら、このソースファイルは削除する
        private static IEnumerable<EquipmentInformation> AllOrders()
        {
            // The following is order summary data
            var data = new ObservableCollection<EquipmentInformation>
            {
                new EquipmentInformation
                {
                    EquipmentName="Windows Phone(Katana2)",
                    LoanDate=new DateTime(2017,02,24),
                    BorrowedMember="返却済み",
                    Remarks="紛失中"
                },
                new EquipmentInformation
                {
                    EquipmentName="Let's Note",
                    LoanDate=new DateTime(2017,8,31),
                    BorrowedMember="Nakamura",
                    Remarks="本人に借りた記憶はない"
                },
                new EquipmentInformation
                {
                    EquipmentName="Windowsタブレット",
                    LoanDate=new DateTime(2017,10,5),
                    BorrowedMember="Nakamura",
                    Remarks="紛失しないよう頑張ります"
                },
                new EquipmentInformation
                {
                    EquipmentName="Windows Phone(MADOSMA Q501)",
                    LoanDate=new DateTime(2016,8,24,23,59,36),
                    BorrowedMember="Haginoya",
                    Remarks="去年から借りっぱなしなんだけど！！！←私物だし！"
                },
            };
            return data;
        }

        // TODO WTS: Remove this once your grid page is displaying real data
        public static ObservableCollection<EquipmentInformation> GetGridSampleData()
        {
            return new ObservableCollection<EquipmentInformation>(AllOrders());
        }
    }
}
