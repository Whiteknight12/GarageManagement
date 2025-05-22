using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCCCDAndGioiTinh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "nhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "nhanViens",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CCCD",
                table: "khachHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GioiTinh",
                table: "khachHangs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("326a7950-188b-4b83-9ae4-1aa4cb18ea5d"), "Admin" },
                    { new Guid("f35806fd-4f7d-4710-8fe6-dcc935bb4db2"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("3beb17c8-825b-45b4-b8b3-63c345afef40"), "admin123", new Guid("326a7950-188b-4b83-9ae4-1aa4cb18ea5d"), "admin" },
                    { new Guid("d5394868-9a02-4bb1-9601-3237c2c4dab0"), "user123", new Guid("f35806fd-4f7d-4710-8fe6-dcc935bb4db2"), "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("326a7950-188b-4b83-9ae4-1aa4cb18ea5d"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("f35806fd-4f7d-4710-8fe6-dcc935bb4db2"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("3beb17c8-825b-45b4-b8b3-63c345afef40"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("d5394868-9a02-4bb1-9601-3237c2c4dab0"));

            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "nhanViens");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "nhanViens");

            migrationBuilder.DropColumn(
                name: "CCCD",
                table: "khachHangs");

            migrationBuilder.DropColumn(
                name: "GioiTinh",
                table: "khachHangs");

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
    }
}
