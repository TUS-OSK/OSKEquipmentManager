using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OSKEquipmentManager.Models;

namespace OSKEquipmentManager.Migrations
{
    [DbContext(typeof(EquipmentInformationContext))]
    partial class EquipmentInformationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("OSKEquipmentManager.Models.EquipmentInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BorrowingMember");

                    b.Property<string>("EquipmentName");

                    b.Property<DateTime>("LastUpdateDate");

                    b.Property<DateTime>("LoanDate");

                    b.Property<string>("Remarks");

                    b.Property<DateTime>("ReturnPlanDate");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("EqInfo");
                });
        }
    }
}
