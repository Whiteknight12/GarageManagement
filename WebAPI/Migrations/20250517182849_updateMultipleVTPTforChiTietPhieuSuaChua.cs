using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateMultipleVTPTforChiTietPhieuSuaChua : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "SoLuong",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.DropColumn(
                name: "VatTuPhuTungId",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.CreateTable(
                name: "vtptChiTietPhieuSuaChuas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChiTietPhieuSuaChuaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatTuPhuTungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vtptChiTietPhieuSuaChuas", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "vtptChiTietPhieuSuaChuas");

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

            migrationBuilder.AddColumn<int>(
                name: "SoLuong",
                table: "chiTietPhieuSuaChuas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "VatTuPhuTungId",
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
    }
}
