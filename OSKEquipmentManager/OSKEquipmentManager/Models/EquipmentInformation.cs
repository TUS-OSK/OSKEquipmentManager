using System;

namespace OSKEquipmentManager.Models
{
    //TODO WTS: このクラスを削除せよ
    // TODO WTS: Remove this class once your pages/features are using your data.
    //この部分はSampleDataServiceによって使われる
    // It is the model class we use to display data on pages like Grid, Chart, and Master Detail.
    public class EquipmentInformation
    {
        public string EquipmentName { get; set; } = "";

        public DateTime LoanDate { get; set; }

        public string BorrowedMember { get; set; }

        public string Remarks { get; set; } = "";

        public void SetProperties(EquipmentInformation source)
        {

        }
    }
}
