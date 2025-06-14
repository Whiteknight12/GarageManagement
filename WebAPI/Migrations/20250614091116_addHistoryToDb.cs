using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addHistoryToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "lichSus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ThoiDiemThucHien = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThucTheLienQuanId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoaiThucTheLienQuan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HanhDong = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lichSus", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"), "Admin" },
                    { new Guid("ac584869-c019-4d17-bdfa-b35243317745"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("c0be6377-3f5e-40cc-b405-5bde3bf0406d"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("ac584869-c019-4d17-bdfa-b35243317745"), "user" },
                    { new Guid("dbe5dac9-4385-4353-8495-579b1f36be5e"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lichSus");

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("81a5c256-7961-4fd9-b3b2-f95b56fa5166"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("ac584869-c019-4d17-bdfa-b35243317745"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("c0be6377-3f5e-40cc-b405-5bde3bf0406d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("dbe5dac9-4385-4353-8495-579b1f36be5e"));

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
    }
}
