using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGenderNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "nhanViens",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "khachHangs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "nhanViens",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GioiTinh",
                table: "khachHangs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
    }
}
