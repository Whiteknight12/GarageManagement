using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class addhieuxetodb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DIACHI",
                table: "CAR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DIENTHOAI",
                table: "CAR",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "hieuXes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenHieuXe = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hieuXes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "hieuXes");

            migrationBuilder.DropColumn(
                name: "DIACHI",
                table: "CAR");

            migrationBuilder.DropColumn(
                name: "DIENTHOAI",
                table: "CAR");
        }
    }
}
