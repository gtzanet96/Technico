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
                    b.Property<int>("PropertiesId")
                        .HasColumnType("int");

                    b.Property<int>("PropertyOwnersId")
                        .HasColumnType("int");

                    b.HasKey("PropertiesId", "PropertyOwnersId");

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

                    b.Property<string>("PropertyIdentificationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

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

            modelBuilder.Entity("Technico.Models.Repair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Cost")
                        .HasPrecision(8, 2)
                        .HasColumnType("decimal(8,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PropertyItemId")
                        .HasColumnType("int");

                    b.Property<int?>("PropertyOwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ScheduledDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PropertyItemId");

                    b.HasIndex("PropertyOwnerId");

                    b.ToTable("Repairs", (string)null);
                });

            modelBuilder.Entity("PropertyItemPropertyOwner", b =>
                {
                    b.HasOne("Technico.Models.PropertyItem", null)
                        .WithMany()
                        .HasForeignKey("PropertiesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technico.Models.PropertyOwner", null)
                        .WithMany()
                        .HasForeignKey("PropertyOwnersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Technico.Models.Repair", b =>
                {
                    b.HasOne("Technico.Models.PropertyItem", "PropertyItem")
                        .WithMany("Repairs")
                        .HasForeignKey("PropertyItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Technico.Models.PropertyOwner", "PropertyOwner")
                        .WithMany("Repairs")
                        .HasForeignKey("PropertyOwnerId");

                    b.Navigation("PropertyItem");

                    b.Navigation("PropertyOwner");
                });

            modelBuilder.Entity("Technico.Models.PropertyItem", b =>
                {
                    b.Navigation("Repairs");
                });

            modelBuilder.Entity("Technico.Models.PropertyOwner", b =>
                {
                    b.Navigation("Repairs");
                });
#pragma warning restore 612, 618
        }
    }
}
