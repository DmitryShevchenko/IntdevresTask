﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI;

namespace WebAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20190926094241_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebAPI.Models.DataModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CpuLoad");

                    b.Property<string>("OS");

                    b.Property<string>("PcName");

                    b.Property<string>("RamLoad");

                    b.HasKey("Id");

                    b.ToTable("PcData");
                });

            modelBuilder.Entity("WebAPI.Models.LoggedUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("DataModelPKey");

                    b.Property<DateTime>("LogginDate");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.HasIndex("DataModelPKey");

                    b.ToTable("LoggedUser");
                });

            modelBuilder.Entity("WebAPI.Models.Manufacturer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CPU");

                    b.Property<Guid>("DataModelPKey");

                    b.Property<string>("GPU");

                    b.Property<string>("MotherBoard");

                    b.HasKey("Id");

                    b.HasIndex("DataModelPKey")
                        .IsUnique();

                    b.ToTable("Manufacturer");
                });

            modelBuilder.Entity("WebAPI.Models.LoggedUser", b =>
                {
                    b.HasOne("WebAPI.Models.DataModel", "DataModel")
                        .WithMany("Users")
                        .HasForeignKey("DataModelPKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Manufacturer", b =>
                {
                    b.HasOne("WebAPI.Models.DataModel", "DataModel")
                        .WithOne("Manufacturer")
                        .HasForeignKey("WebAPI.Models.Manufacturer", "DataModelPKey")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
