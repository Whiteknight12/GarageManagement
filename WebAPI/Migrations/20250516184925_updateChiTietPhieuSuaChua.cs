using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateChiTietPhieuSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("6a2ad280-1ab7-47bf-a998-3b03e3f29a9c"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("cb05521b-31cd-4aea-a773-a989191579b5"));

            migrationBuilder.DropColumn(
                name: "NoiDung",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.AddColumn<Guid>(
                name: "NoiDungSuaChuaId",
                table: "chiTietPhieuSuaChuas",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("5e67a760-9f8c-4dfa-9a5d-c2f4087a18b2"), "User" },
                    { new Guid("8687b195-93d6-40e7-888f-4fb19ed16789"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("67df2516-24a8-446d-86f6-0886ddaf0100"), "admin123", new Guid("8687b195-93d6-40e7-888f-4fb19ed16789"), "admin" },
                    { new Guid("be73394f-0bb6-403e-9671-f3b835a68abc"), "user123", new Guid("5e67a760-9f8c-4dfa-9a5d-c2f4087a18b2"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("5e67a760-9f8c-4dfa-9a5d-c2f4087a18b2"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("8687b195-93d6-40e7-888f-4fb19ed16789"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("67df2516-24a8-446d-86f6-0886ddaf0100"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("be73394f-0bb6-403e-9671-f3b835a68abc"));

            migrationBuilder.DropColumn(
                name: "NoiDungSuaChuaId",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.AddColumn<string>(
                name: "NoiDung",
                table: "chiTietPhieuSuaChuas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"), "User" },
                    { new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("6a2ad280-1ab7-47bf-a998-3b03e3f29a9c"), "user123", new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"), "user" },
                    { new Guid("cb05521b-31cd-4aea-a773-a989191579b5"), "admin123", new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"), "admin" }
                });
        }
    }
}
