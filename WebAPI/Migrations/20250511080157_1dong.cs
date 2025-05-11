using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class _1dong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("3cfc2988-299e-49be-8fc2-49168f286ebd"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("91881c7a-2f24-470d-8b7b-704cba0a6156"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("0ab7afbe-a155-432f-8478-c77bbd13f33d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("572c0ec7-93bf-4bd1-8151-f9fcb2c93c76"));

            migrationBuilder.DropColumn(
                name: "DonGia",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.CreateTable(
                name: "baoCaoDoanhThuThangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false),
                    TongDoanhThu = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baoCaoDoanhThuThangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chiTietBaoCaoDoanhThuThangs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaoCaoDoanhThuThangId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HieuXeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuotSua = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false),
                    TiLe = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietBaoCaoDoanhThuThangs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chiTietPhieuNhapVatTus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PhieuNhapId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatTuId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DonGia = table.Column<double>(type: "float", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietPhieuNhapVatTus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "phieuNhapVatTus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieuNhapVatTus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"), "User" },
                    { new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("46347bea-7dc3-4e02-ae1f-e622f0280697"), "admin123", new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"), "admin" },
                    { new Guid("969a4ef5-8efd-4db5-817f-be87bc7d13c9"), "user123", new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baoCaoDoanhThuThangs");

            migrationBuilder.DropTable(
                name: "chiTietBaoCaoDoanhThuThangs");

            migrationBuilder.DropTable(
                name: "chiTietPhieuNhapVatTus");

            migrationBuilder.DropTable(
                name: "phieuNhapVatTus");

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("46347bea-7dc3-4e02-ae1f-e622f0280697"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("969a4ef5-8efd-4db5-817f-be87bc7d13c9"));

            migrationBuilder.AddColumn<double>(
                name: "DonGia",
                table: "chiTietPhieuSuaChuas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("3cfc2988-299e-49be-8fc2-49168f286ebd"), "User" },
                    { new Guid("91881c7a-2f24-470d-8b7b-704cba0a6156"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("0ab7afbe-a155-432f-8478-c77bbd13f33d"), "admin123", new Guid("91881c7a-2f24-470d-8b7b-704cba0a6156"), "admin" },
                    { new Guid("572c0ec7-93bf-4bd1-8151-f9fcb2c93c76"), "user123", new Guid("3cfc2988-299e-49be-8fc2-49168f286ebd"), "user" }
                });
        }
    }
}
