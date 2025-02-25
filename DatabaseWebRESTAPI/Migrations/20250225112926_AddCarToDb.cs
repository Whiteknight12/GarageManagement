using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseWebRESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddCarToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAR",
                columns: table => new
                {
                    CarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OwnerPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CarBrand = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CarNumber = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAR", x => x.CarID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAR");
        }
    }
}
