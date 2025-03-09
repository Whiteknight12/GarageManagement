using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatTuPhuTung",
                table: "noiDungPhieuSuaChuas");

            migrationBuilder.AddColumn<int>(
                name: "VatTuPhuTungID",
                table: "noiDungPhieuSuaChuas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VatTuPhuTungID",
                table: "noiDungPhieuSuaChuas");

            migrationBuilder.AddColumn<string>(
                name: "VatTuPhuTung",
                table: "noiDungPhieuSuaChuas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
