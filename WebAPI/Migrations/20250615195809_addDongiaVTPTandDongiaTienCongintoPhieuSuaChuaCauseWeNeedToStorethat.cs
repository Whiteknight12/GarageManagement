using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addDongiaVTPTandDongiaTienCongintoPhieuSuaChuaCauseWeNeedToStorethat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("6c1fd300-f2d6-434e-8a27-e7faacb4c6c3"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("a06c587f-d9da-406c-b4b1-4f129c95a606"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("2749dcf3-bdd9-47c1-b512-455c33493008"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("5d7b19b1-c904-41a1-b6ae-7075f6a13632"));

            migrationBuilder.AddColumn<double>(
                name: "DonGiaVTPTApDung",
                table: "vtptChiTietPhieuSuaChuas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "TienCongApDung",
                table: "chiTietPhieuSuaChuas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("64e24bdb-de4f-4ba2-b7ef-e37e5d4a9f0c"), "User" },
                    { new Guid("ee0d6b19-2cd7-44e1-8da1-a8f2cba7d85e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("17bd6038-b700-460b-9af7-3523543a1f6d"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("64e24bdb-de4f-4ba2-b7ef-e37e5d4a9f0c"), "user" },
                    { new Guid("859e3bad-c73f-47f4-87f2-3bfad6a477bf"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ee0d6b19-2cd7-44e1-8da1-a8f2cba7d85e"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("64e24bdb-de4f-4ba2-b7ef-e37e5d4a9f0c"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("ee0d6b19-2cd7-44e1-8da1-a8f2cba7d85e"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("17bd6038-b700-460b-9af7-3523543a1f6d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("859e3bad-c73f-47f4-87f2-3bfad6a477bf"));

            migrationBuilder.DropColumn(
                name: "DonGiaVTPTApDung",
                table: "vtptChiTietPhieuSuaChuas");

            migrationBuilder.DropColumn(
                name: "TienCongApDung",
                table: "chiTietPhieuSuaChuas");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("6c1fd300-f2d6-434e-8a27-e7faacb4c6c3"), "User" },
                    { new Guid("a06c587f-d9da-406c-b4b1-4f129c95a606"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("2749dcf3-bdd9-47c1-b512-455c33493008"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("6c1fd300-f2d6-434e-8a27-e7faacb4c6c3"), "user" },
                    { new Guid("5d7b19b1-c904-41a1-b6ae-7075f6a13632"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("a06c587f-d9da-406c-b4b1-4f129c95a606"), "admin" }
                });
        }
    }
}
