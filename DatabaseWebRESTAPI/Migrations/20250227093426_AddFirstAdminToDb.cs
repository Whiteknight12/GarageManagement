using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatabaseWebRESTAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddFirstAdminToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "USER_ACCOUNT",
                columns: new[] { "UserAccountID", "Password", "Role", "Username" },
                values: new object[] { 1, "$2a$11$TDDB6C/PpAeHNQKTO4p6qOmZw9B7GZX1g.fmFqZmr4YVGVhJZ5FSO", "Admin", "admin1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "USER_ACCOUNT",
                keyColumn: "UserAccountID",
                keyValue: 1);
        }
    }
}
