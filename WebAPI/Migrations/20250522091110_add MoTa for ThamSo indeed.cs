using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addMoTaforThamSoindeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("802b90da-c327-43d4-bf8c-dda5c5d65b49"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("f149931e-6c92-4949-b1dd-382056813a95"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("6c4576be-d6bf-4583-8c18-7e54077174b3"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("a6406fa3-b3df-41cb-a5d2-5e2a614803cb"));

            migrationBuilder.AddColumn<string>(
                name: "MoTa",
                table: "thamSos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "MoTa",
                table: "thamSos");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("802b90da-c327-43d4-bf8c-dda5c5d65b49"), "User" },
                    { new Guid("f149931e-6c92-4949-b1dd-382056813a95"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("6c4576be-d6bf-4583-8c18-7e54077174b3"), "admin123", new Guid("f149931e-6c92-4949-b1dd-382056813a95"), "admin" },
                    { new Guid("a6406fa3-b3df-41cb-a5d2-5e2a614803cb"), "user123", new Guid("802b90da-c327-43d4-bf8c-dda5c5d65b49"), "user" }
                });
        }
    }
}
