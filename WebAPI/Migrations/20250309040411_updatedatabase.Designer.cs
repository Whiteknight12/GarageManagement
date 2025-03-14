﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Data;

#nullable disable

namespace WebAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250309040411_updatedatabase")]
    partial class updatedatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPI.Models.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BienSo")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("BIENSO");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("MODEL");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("NAME");

                    b.Property<string>("TenChuXe")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("TENCHUXE");

                    b.HasKey("Id");

                    b.ToTable("CAR");
                });

            modelBuilder.Entity("WebAPI.Models.CarRecord", b =>
                {
                    b.Property<int>("CarRecordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CarRecordID"));

                    b.Property<string>("BienSo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiaChi")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HieuXe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("NgayTiepNhan")
                        .HasColumnType("datetime2");

                    b.Property<string>("SoDienThoai")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenChuXe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenXe")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CarRecordID");

                    b.ToTable("CARRECORD");
                });

            modelBuilder.Entity("WebAPI.Models.RuleVariable", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("SoXeTiepNhanToiDaMotNgay")
                        .HasColumnType("int")
                        .HasColumnName("SOXETIEPNHANTOIDAMOTNGAY");

                    b.HasKey("Id");

                    b.ToTable("RULEVARIABLE");
                });

            modelBuilder.Entity("WebAPI.Models.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("PASSWORD");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("ROLE");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("USERNAME");

                    b.HasKey("Id");

                    b.ToTable("USERACCOUNT");
                });
#pragma warning restore 612, 618
        }
    }
}
