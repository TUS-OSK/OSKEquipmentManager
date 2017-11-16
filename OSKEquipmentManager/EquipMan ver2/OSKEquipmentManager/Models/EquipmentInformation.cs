using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace OSKEquipmentManager.Models
{
    public enum EquipmentStatus
    {
        利用可能,
        貸出中,
        紛失中
    }

    public class EquipmentInformationContext:DbContext
    {
        public DbSet<EquipmentInformation> EqInfo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=blogging.db");
        }
    }

    //TODO WTS: このクラスを削除せよ
    // TODO WTS: Remove this class once your pages/features are using your data.
    //この部分はSampleDataServiceによって使われる
    // It is the model class we use to display data on pages like Grid, Chart, and Master Detail.
    public class EquipmentInformation
    {
        //備品を識別するための主キー
        public int Id { get; set; }

        public string EquipmentName { get; set; } = "";

        public EquipmentStatus Status { get; set; }

        public DateTime ReturnPlanDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public DateTime LoanDate { get; set; }

        public string BorrowingMember { get; set; }

        public string Remarks { get; set; } = "";
    }
}
