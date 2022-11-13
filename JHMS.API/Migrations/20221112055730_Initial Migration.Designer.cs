﻿// <auto-generated />
using JHMS.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JHMS.API.Migrations
{
    [DbContext(typeof(JHMSDbContext))]
    [Migration("20221112055730_Initial Migration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("JHMS.API.Models.Customer", b =>
                {
                    b.Property<int>("intCustomerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("intCustomerID"), 1L, 1);

                    b.Property<string>("strCustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("intCustomerID");

                    b.ToTable("TCustomers");
                });
#pragma warning restore 612, 618
        }
    }
}
