using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addNoiDungSuaChuaToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("2f2ed88b-dd5f-4404-91e8-8a0d0344dcd6"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("fb756614-a241-44a5-acf6-2d7b7238e95e"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("a2eaaa8f-28a6-4aa7-9ccb-25038c87aec5"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("e54f5db4-facd-4e52-b6c3-ffa804b0249b"));

            migrationBuilder.AddColumn<Guid>(
                name: "NoiDungSuaChuaId",
                table: "tienCongs",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "noiDungSuaChuas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenNoiDungSuaChua = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_noiDungSuaChuas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"), "User" },
                    { new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("6a2ad280-1ab7-47bf-a998-3b03e3f29a9c"), "user123", new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"), "user" },
                    { new Guid("cb05521b-31cd-4aea-a773-a989191579b5"), "admin123", new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"), "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "noiDungSuaChuas");

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("111f3fcf-217d-4a76-94ab-18d031f543af"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("6e6f9b96-75f3-43a5-ace7-5b827551c08d"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("6a2ad280-1ab7-47bf-a998-3b03e3f29a9c"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("cb05521b-31cd-4aea-a773-a989191579b5"));

            migrationBuilder.DropColumn(
                name: "NoiDungSuaChuaId",
                table: "tienCongs");

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("2f2ed88b-dd5f-4404-91e8-8a0d0344dcd6"), "User" },
                    { new Guid("fb756614-a241-44a5-acf6-2d7b7238e95e"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("a2eaaa8f-28a6-4aa7-9ccb-25038c87aec5"), "admin123", new Guid("fb756614-a241-44a5-acf6-2d7b7238e95e"), "admin" },
                    { new Guid("e54f5db4-facd-4e52-b6c3-ffa804b0249b"), "user123", new Guid("2f2ed88b-dd5f-4404-91e8-8a0d0344dcd6"), "user" }
                });
        }
    }
}
