using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgeter.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedDateColumnInTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Transactions",
                newName: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Transactions",
                newName: "DateTime");
        }
    }
}
