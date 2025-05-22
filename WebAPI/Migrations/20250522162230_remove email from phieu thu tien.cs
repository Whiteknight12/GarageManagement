using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class removeemailfromphieuthutien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("76d3f152-4de1-416b-8832-c4e9daaa61e5"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("e94a972d-2548-47b9-be32-6f02071335f2"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "phieuThuTiens");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("5e8114e7-827b-4fb9-b0ac-6f94a9b187dd"), "Admin" },
                    { new Guid("e7dc97df-4e8d-473d-be5e-5f233c62d820"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("658472d2-d8b6-475e-b230-8336500b9850"), "admin123", new Guid("5e8114e7-827b-4fb9-b0ac-6f94a9b187dd"), "admin" },
                    { new Guid("b8a287ec-be7a-4416-bceb-0f48f01c58d4"), "user123", new Guid("e7dc97df-4e8d-473d-be5e-5f233c62d820"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("5e8114e7-827b-4fb9-b0ac-6f94a9b187dd"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("e7dc97df-4e8d-473d-be5e-5f233c62d820"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("658472d2-d8b6-475e-b230-8336500b9850"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("b8a287ec-be7a-4416-bceb-0f48f01c58d4"));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "phieuThuTiens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"), "User" },
                    { new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("76d3f152-4de1-416b-8832-c4e9daaa61e5"), "admin123", new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"), "admin" },
                    { new Guid("e94a972d-2548-47b9-be32-6f02071335f2"), "user123", new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"), "user" }
                });
        }
    }
}
