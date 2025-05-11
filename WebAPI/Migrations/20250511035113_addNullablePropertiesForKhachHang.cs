using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNullablePropertiesForKhachHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("8019d799-44fc-4e93-9c30-d78b3eafbcf5"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("805a9986-dc88-404e-8e4f-3e5fedb13d47"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("0fb2f35f-f76b-4fdd-9c6a-06aaf83b3a62"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("117decf4-0163-4bca-91b6-cb98001a54b9"));

            migrationBuilder.AlterColumn<int>(
                name: "Tuoi",
                table: "khachHangs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<int>(
                name: "Tuoi",
                table: "khachHangs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
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
                    { new Guid("8019d799-44fc-4e93-9c30-d78b3eafbcf5"), "User" },
                    { new Guid("805a9986-dc88-404e-8e4f-3e5fedb13d47"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("0fb2f35f-f76b-4fdd-9c6a-06aaf83b3a62"), "admin123", new Guid("805a9986-dc88-404e-8e4f-3e5fedb13d47"), "admin" },
                    { new Guid("117decf4-0163-4bca-91b6-cb98001a54b9"), "user123", new Guid("8019d799-44fc-4e93-9c30-d78b3eafbcf5"), "user" }
                });
        }
    }
}
