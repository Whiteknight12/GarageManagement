using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addMoTaforThamSo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("ad5fe95c-5554-488f-9db6-3fd605fe3bda"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("d9ca3f3e-f4fb-4ef6-9abe-15dc9aad7eec"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("c7180c93-07c1-4bd0-abc6-f2689ff1ccd8"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("ca2e1202-5959-451c-8b39-2d0d3db06ea6"));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("ad5fe95c-5554-488f-9db6-3fd605fe3bda"), "Admin" },
                    { new Guid("d9ca3f3e-f4fb-4ef6-9abe-15dc9aad7eec"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("c7180c93-07c1-4bd0-abc6-f2689ff1ccd8"), "user123", new Guid("d9ca3f3e-f4fb-4ef6-9abe-15dc9aad7eec"), "user" },
                    { new Guid("ca2e1202-5959-451c-8b39-2d0d3db06ea6"), "admin123", new Guid("ad5fe95c-5554-488f-9db6-3fd605fe3bda"), "admin" }
                });
        }
    }
}
