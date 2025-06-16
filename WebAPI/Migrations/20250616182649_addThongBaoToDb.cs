using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addThongBaoToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "nguoiDungThongBaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    thongBaoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    nguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nguoiDungThongBaos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "thongBaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NhomNguoiDungId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isForAll = table.Column<bool>(type: "bit", nullable: true),
                    taoVaoLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    xeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_thongBaos", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nguoiDungThongBaos");

            migrationBuilder.DropTable(
                name: "thongBaos");

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
    }
}
