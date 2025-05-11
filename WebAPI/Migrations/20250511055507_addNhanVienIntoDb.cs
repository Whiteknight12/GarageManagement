using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNhanVienIntoDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("3be2914d-cfee-4ba5-9d89-2b53ad3e4954"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("c119a7ee-d76c-42f4-9002-e0229c49618e"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("4c3e80ca-2aa6-4046-ab92-a83ac9d61fc3"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("f7de66f4-2cc8-446b-9e43-968fd9b4921d"));

            migrationBuilder.DropColumn(
                name: "DaCoTaiKhoan",
                table: "khachHangs");

            migrationBuilder.DropColumn(
                name: "NhomNguoiDungId",
                table: "khachHangs");

            migrationBuilder.AddColumn<Guid>(
                name: "TaiKhoanId",
                table: "khachHangs",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "nhanViens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tuoi = table.Column<int>(type: "int", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaiKhoanId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nhanViens", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"), "User" },
                    { new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("f6173b61-2f0c-4e76-9277-c13122bde7ec"), "user123", new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"), "user" },
                    { new Guid("fb651f58-924e-4ba4-933a-db601e4abf31"), "admin123", new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "nhanViens");

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("5ac25c4c-7653-4a4f-90c5-22a33d9e4ac2"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("63b3bbb3-fd9a-4fbe-ae0f-8031ea7ca4fb"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("f6173b61-2f0c-4e76-9277-c13122bde7ec"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("fb651f58-924e-4ba4-933a-db601e4abf31"));

            migrationBuilder.DropColumn(
                name: "TaiKhoanId",
                table: "khachHangs");

            migrationBuilder.AddColumn<bool>(
                name: "DaCoTaiKhoan",
                table: "khachHangs",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "NhomNguoiDungId",
                table: "khachHangs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("3be2914d-cfee-4ba5-9d89-2b53ad3e4954"), "Admin" },
                    { new Guid("c119a7ee-d76c-42f4-9002-e0229c49618e"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("4c3e80ca-2aa6-4046-ab92-a83ac9d61fc3"), "user123", new Guid("c119a7ee-d76c-42f4-9002-e0229c49618e"), "user" },
                    { new Guid("f7de66f4-2cc8-446b-9e43-968fd9b4921d"), "admin123", new Guid("3be2914d-cfee-4ba5-9d89-2b53ad3e4954"), "admin" }
                });
        }
    }
}
