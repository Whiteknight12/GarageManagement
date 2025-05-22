using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class chinhsuakieudulieugiatrithamsointtodouble : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("40a4a728-66b5-493f-8362-fd0ad9fa7e70"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("ba82589d-6b3c-4444-8558-622da49e62a9"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("086289eb-370d-49e9-9809-38f298f8357d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("9cf6216e-0145-4506-9362-4fc9e8c2d4d6"));

            migrationBuilder.AlterColumn<double>(
                name: "GiaTri",
                table: "thamSos",
                type: "float",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("9b239800-d3ee-4883-8cf5-fd6c2132805f"), "User" },
                    { new Guid("f13f75cb-cbed-4b83-884a-72cec66d63f6"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("47a4d270-eeda-45d9-83fc-6fa57c00a199"), "admin123", new Guid("f13f75cb-cbed-4b83-884a-72cec66d63f6"), "admin" },
                    { new Guid("5b30c894-8840-4e00-9edd-6b3a4f3c07d0"), "user123", new Guid("9b239800-d3ee-4883-8cf5-fd6c2132805f"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("9b239800-d3ee-4883-8cf5-fd6c2132805f"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("f13f75cb-cbed-4b83-884a-72cec66d63f6"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("47a4d270-eeda-45d9-83fc-6fa57c00a199"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("5b30c894-8840-4e00-9edd-6b3a4f3c07d0"));

            migrationBuilder.AlterColumn<int>(
                name: "GiaTri",
                table: "thamSos",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("40a4a728-66b5-493f-8362-fd0ad9fa7e70"), "Admin" },
                    { new Guid("ba82589d-6b3c-4444-8558-622da49e62a9"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("086289eb-370d-49e9-9809-38f298f8357d"), "user123", new Guid("ba82589d-6b3c-4444-8558-622da49e62a9"), "user" },
                    { new Guid("9cf6216e-0145-4506-9362-4fc9e8c2d4d6"), "admin123", new Guid("40a4a728-66b5-493f-8362-fd0ad9fa7e70"), "admin" }
                });
        }
    }
}
