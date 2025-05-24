using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class update_ngaycaptaikhoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCap",
                table: "taiKhoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("d9ed7542-25d7-4700-b38d-3bfd618d7b3f"), "User" },
                    { new Guid("f2c069c6-ef49-4608-9fd8-4e813c43ab19"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("b1dcca1b-de2b-4745-93f4-ba4c31d42e2c"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("d9ed7542-25d7-4700-b38d-3bfd618d7b3f"), "user" },
                    { new Guid("d4964571-f33c-4ae9-9f70-a98867478951"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("f2c069c6-ef49-4608-9fd8-4e813c43ab19"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("d9ed7542-25d7-4700-b38d-3bfd618d7b3f"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("f2c069c6-ef49-4608-9fd8-4e813c43ab19"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("b1dcca1b-de2b-4745-93f4-ba4c31d42e2c"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("d4964571-f33c-4ae9-9f70-a98867478951"));

            migrationBuilder.DropColumn(
                name: "NgayCap",
                table: "taiKhoans");

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
    }
}
