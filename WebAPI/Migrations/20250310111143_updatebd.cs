using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class updatebd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chiTietTienCongs");

            migrationBuilder.DropColumn(
                name: "TienCong",
                table: "noiDungPhieuSuaChuas");

            migrationBuilder.AddColumn<int>(
                name: "TienCongID",
                table: "noiDungPhieuSuaChuas",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TienCongID",
                table: "noiDungPhieuSuaChuas");

            migrationBuilder.AddColumn<double>(
                name: "TienCong",
                table: "noiDungPhieuSuaChuas",
                type: "float",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "chiTietTienCongs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NoiDungPhieuSuaChuaID = table.Column<int>(type: "int", nullable: true),
                    TienCongID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chiTietTienCongs", x => x.ID);
                });
        }
    }
}
