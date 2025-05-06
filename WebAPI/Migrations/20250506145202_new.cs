using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chiTietPhieuSuaChuas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhieuSuaChuaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatTuPhuTungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TienCongId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietPhieuSuaChuas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chucNangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenChucNang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenManHinhDuocLoad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chucNangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hieuXes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenHieuXe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hieuXes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "khachHangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoVaTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuoi = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaCoTaiKhoan = table.Column<bool>(type: "bit", nullable: false),
                    TienNo = table.Column<double>(type: "float", nullable: false),
                    NhomNguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_khachHangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nhomNguoiDungs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNhom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhomNguoiDungs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phanQuyens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NhomNguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChucNangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phanQuyens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phieuSuaChuas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgaySuaChua = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieuSuaChuas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phieuThuTiens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhachHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    XeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayThuTien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTienThu = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieuThuTiens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phieuTiepNhans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayTiepNhan = table.Column<DateTime>(type: "datetime2", nullable: true),
                    XeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieuTiepNhans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "taiKhoans",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NhomNguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_taiKhoans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thamSos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenThamSo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTri = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thamSos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tienCongs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenLoaiTienCong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGiaLoaiTienCong = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tienCongs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vatTuPhuTungs",
                columns: table => new
                {
                    VatTuPhuTungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenLoaiVatTuPhuTung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DonGiaNhapLoaiVatTuPhuTung = table.Column<double>(type: "float", nullable: false),
                    DonGiaBanLoaiVatTuPhuTung = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vatTuPhuTungs", x => x.VatTuPhuTungId);
                });

            migrationBuilder.CreateTable(
                name: "xes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HieuXeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BienSo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KhaDung = table.Column<bool>(type: "bit", nullable: true),
                    TienNo = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_xes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("8019d799-44fc-4e93-9c30-d78b3eafbcf5"), "User" },
                    { new Guid("805a9986-dc88-404e-8e4f-3e5fedb13d47"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("0fb2f35f-f76b-4fdd-9c6a-06aaf83b3a62"), "admin123", new Guid("805a9986-dc88-404e-8e4f-3e5fedb13d47"), "admin" },
                    { new Guid("117decf4-0163-4bca-91b6-cb98001a54b9"), "user123", new Guid("8019d799-44fc-4e93-9c30-d78b3eafbcf5"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietPhieuSuaChuas");

            migrationBuilder.DropTable(
                name: "chucNangs");

            migrationBuilder.DropTable(
                name: "hieuXes");

            migrationBuilder.DropTable(
                name: "khachHangs");

            migrationBuilder.DropTable(
                name: "nhomNguoiDungs");

            migrationBuilder.DropTable(
                name: "phanQuyens");

            migrationBuilder.DropTable(
                name: "phieuSuaChuas");

            migrationBuilder.DropTable(
                name: "phieuThuTiens");

            migrationBuilder.DropTable(
                name: "phieuTiepNhans");

            migrationBuilder.DropTable(
                name: "taiKhoans");

            migrationBuilder.DropTable(
                name: "thamSos");

            migrationBuilder.DropTable(
                name: "tienCongs");

            migrationBuilder.DropTable(
                name: "vatTuPhuTungs");

            migrationBuilder.DropTable(
                name: "xes");
        }
    }
}
