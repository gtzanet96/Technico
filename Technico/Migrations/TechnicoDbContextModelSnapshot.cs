﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Technico.Repositories;

#nullable disable

namespace Technico.Migrations
{
    [DbContext(typeof(TechnicoDbContext))]
    partial class TechnicoDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PropertyItemPropertyOwner", b =>
                {
                    b.Property<int>("PropertyItemsId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyOwnersId")
                        .HasColumnType("int");

                    b.HasKey("PropertyItemsId", "PropertyOwnersId");

                    b.HasIndex("PropertyOwnersId");

                    b.ToTable("PropertyItemPropertyOwner");
                });

            modelBuilder.Entity("Technico.Models.PropertyItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("bit");

                    b.Property<string>("PropertyAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PropertyIdentificationNumber")
                        .HasColumnType("bigint");

                    b.Property<int>("PropertyType")
                        .HasColumnType("int");

                    b.Property<int>("YearOfConstruction")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PropertyIdentificationNumber")
                        .IsUnique();

                    b.ToTable("PropertyItems", (string)null);
                });

            modelBuilder.Entity("Technico.Models.PropertyOwner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VAT")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("VAT")
                        .IsUnique();

                    b.ToTable("PropertyOwners", (string)null);
                });

            modelBuilder.Entity("Technico.Models.PropertyRepair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<bool>("IsDeactivated")
                        .HasColumnType("bit");

                    b.Property<int?>("PropertyItemId")
                        .HasColumnType("int");

                    b.Property<string>("RepairDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RepairStatus")
                        .HasColumnType("int");

                    b.Property<int>("RepairType")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("PropertyItemId");

                    b.ToTable("PropertyRepairs", (string)null);
                });

            modelBuilder.Entity("PropertyItemPropertyOwner", b =>
                {
                    b.HasOne("Technico.Models.PropertyItem", null)
                        .WithMany()
                        .HasForeignKey("PropertyItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technico.Models.PropertyOwner", null)
                        .WithMany()
                        .HasForeignKey("PropertyOwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Technico.Models.PropertyRepair", b =>
                {
                    b.HasOne("Technico.Models.PropertyItem", "PropertyItem")
                        .WithMany("Repairs")
                        .HasForeignKey("PropertyItemId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("PropertyItem");
                });

            modelBuilder.Entity("Technico.Models.PropertyItem", b =>
                {
                    b.Navigation("Repairs");
                });
#pragma warning restore 612, 618
        }
    }
}
