using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class tungtungtungsahur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"));

            migrationBuilder.DeleteData(
                table: "nhomNguoiDungs",
                keyColumn: "Id",
                keyValue: new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("46347bea-7dc3-4e02-ae1f-e622f0280697"));

            migrationBuilder.DeleteData(
                table: "taiKhoans",
                keyColumn: "Id",
                keyValue: new Guid("969a4ef5-8efd-4db5-817f-be87bc7d13c9"));

            migrationBuilder.CreateTable(
                name: "baoCaoTons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Thang = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baoCaoTons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "chiTietBaoCaoTons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VatTuPhuTungId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BaoCaoTonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TonDau = table.Column<int>(type: "int", nullable: false),
                    PhatSinh = table.Column<int>(type: "int", nullable: false),
                    TonCuoi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietBaoCaoTons", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "baoCaoTons");

            migrationBuilder.DropTable(
                name: "chiTietBaoCaoTons");

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

            migrationBuilder.InsertData(
                table: "nhomNguoiDungs",
                columns: new[] { "Id", "TenNhom" },
                values: new object[,]
                {
                    { new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"), "User" },
                    { new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "taiKhoans",
                columns: new[] { "Id", "MatKhau", "NhomNguoiDungId", "TenDangNhap" },
                values: new object[,]
                {
                    { new Guid("46347bea-7dc3-4e02-ae1f-e622f0280697"), "admin123", new Guid("d8689e6c-1b76-4b45-9e8d-2c08d3cd82d6"), "admin" },
                    { new Guid("969a4ef5-8efd-4db5-817f-be87bc7d13c9"), "user123", new Guid("b1292849-fc84-4c55-8fc1-32ac018f053a"), "user" }
                });
        }
    }
}
