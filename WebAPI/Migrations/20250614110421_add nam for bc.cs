using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addnamforbc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("ac584869-c019-4d17-bdfa-b35243317745"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("c0be6377-3f5e-40cc-b405-5bde3bf0406d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("dbe5dac9-4385-4353-8495-579b1f36be5e"));

            migrationBuilder.AddColumn<int>(
                name: "Nam",
                table: "baoCaoDoanhThuThangs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("c96caccc-2b52-4a28-b2b5-72e84f4e73a8"), "User" },
                    { new Guid("d9841466-1801-4dcc-8293-ceb3f6de1920"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("02c78bcb-4a1b-4f71-91e1-afd1e69c3322"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d9841466-1801-4dcc-8293-ceb3f6de1920"), "admin" },
                    { new Guid("98f05182-5448-4423-ba08-fb718cf9b1c8"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("c96caccc-2b52-4a28-b2b5-72e84f4e73a8"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("c96caccc-2b52-4a28-b2b5-72e84f4e73a8"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("d9841466-1801-4dcc-8293-ceb3f6de1920"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("02c78bcb-4a1b-4f71-91e1-afd1e69c3322"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("98f05182-5448-4423-ba08-fb718cf9b1c8"));

            migrationBuilder.DropColumn(
                name: "Nam",
                table: "baoCaoDoanhThuThangs");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"), "Admin" },
                    { new Guid("ac584869-c019-4d17-bdfa-b35243317745"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("c0be6377-3f5e-40cc-b405-5bde3bf0406d"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ac584869-c019-4d17-bdfa-b35243317745"), "user" },
                    { new Guid("dbe5dac9-4385-4353-8495-579b1f36be5e"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"), "admin" }
                });
        }
    }
}
