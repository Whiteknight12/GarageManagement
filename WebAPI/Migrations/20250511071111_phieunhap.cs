using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class phieunhap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("f6173b61-2f0c-4e76-9277-c13122bde7ec"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("fb651f58-924e-4ba4-933a-db601e4abf31"));

            migrationBuilder.DropColumn(
                name: "DonGiaNhapLoaiVatTuPhuTung",
                table: "vatTuPhuTungs");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "xes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "vatTuPhuTungs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "DaHoanThanhBaoTri",
                table: "phieuTiepNhans",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "ImageUrl",
                table: "xes");

            migrationBuilder.DropColumn(
                name: "SoLuong",
                table: "vatTuPhuTungs");

            migrationBuilder.DropColumn(
                name: "DaHoanThanhBaoTri",
                table: "phieuTiepNhans");

            migrationBuilder.AddColumn<double>(
                name: "DonGiaNhapLoaiVatTuPhuTung",
                table: "vatTuPhuTungs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"), "User" },
                    { new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("f6173b61-2f0c-4e76-9277-c13122bde7ec"), "user123", new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"), "user" },
                    { new Guid("fb651f58-924e-4ba4-933a-db601e4abf31"), "admin123", new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"), "admin" }
                });
        }
    }
}
