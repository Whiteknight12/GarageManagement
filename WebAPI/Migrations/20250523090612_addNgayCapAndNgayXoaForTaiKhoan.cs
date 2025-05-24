using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNgayCapAndNgayXoaForTaiKhoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("76d3f152-4de1-416b-8832-c4e9daaa61e5"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("e94a972d-2548-47b9-be32-6f02071335f2"));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayCap",
                table: "taiKhoans",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayXoa",
                table: "taiKhoans",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("76e37dbf-273b-4eb9-bd49-c3b818c28bfa"), "Admin" },
                    { new Guid("b4333d74-b002-404d-b95a-4c35b7e8b0ad"), "User" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NgayCap", "NgayXoa", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("129178f1-b575-4514-87d6-87d7dfb0558c"), "user123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("b4333d74-b002-404d-b95a-4c35b7e8b0ad"), "user" },
                    { new Guid("43f781d7-653b-4426-978a-d6ce288b63cc"), "admin123", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new Guid("76e37dbf-273b-4eb9-bd49-c3b818c28bfa"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("76e37dbf-273b-4eb9-bd49-c3b818c28bfa"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("b4333d74-b002-404d-b95a-4c35b7e8b0ad"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("129178f1-b575-4514-87d6-87d7dfb0558c"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("43f781d7-653b-4426-978a-d6ce288b63cc"));

            migrationBuilder.DropColumn(
                name: "NgayCap",
                table: "taiKhoans");

            migrationBuilder.DropColumn(
                name: "NgayXoa",
                table: "taiKhoans");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"), "User" },
                    { new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("76d3f152-4de1-416b-8832-c4e9daaa61e5"), "admin123", new Guid("a15b2c4c-d06a-448b-8605-82ba2e3c59b2"), "admin" },
                    { new Guid("e94a972d-2548-47b9-be32-6f02071335f2"), "user123", new Guid("736b4246-8aa9-4d43-98ce-d780394d49d8"), "user" }
                });
        }
    }
}
