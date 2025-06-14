using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addnamforbcton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "Nam",
                table: "baoCaoTons",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Nam",
                table: "baoCaoTons");

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
    }
}
