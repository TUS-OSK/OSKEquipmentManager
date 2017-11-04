using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using OSKEquipmentManager.Models;

namespace OSKEquipmentManager.Migrations
{
    [DbContext(typeof(EquipmentInformationContext))]
    [Migration("20171021043955_EqMigration")]
    partial class EqMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.3");

            modelBuilder.Entity("OSKEquipmentManager.Models.EquipmentInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BorrowingMember");

                    b.Property<string>("EquipmentName");

                    b.Property<DateTime>("LoanDate");

                    b.Property<string>("Remarks");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Informations");
                });
        }
    }
}
