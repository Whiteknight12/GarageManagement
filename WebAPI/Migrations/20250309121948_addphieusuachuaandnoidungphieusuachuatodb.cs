using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addphieusuachuaandnoidungphieusuachuatodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "noiDungPhieuSuaChuas",
                columns: table => new
                {
                    NoiDungID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VatTuPhuTung = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    DonGia = table.Column<double>(type: "float", nullable: true),
                    TienCong = table.Column<double>(type: "float", nullable: true),
                    ThanhTien = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_noiDungPhieuSuaChuas", x => x.NoiDungID);
                });

            migrationBuilder.CreateTable(
                name: "phieuSuaChuas",
                columns: table => new
                {
                    PhieuSuaChuaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BienSoXe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NgaySuaChua = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phieuSuaChuas", x => x.PhieuSuaChuaID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "noiDungPhieuSuaChuas");

            migrationBuilder.DropTable(
                name: "phieuSuaChuas");
        }
    }
}
