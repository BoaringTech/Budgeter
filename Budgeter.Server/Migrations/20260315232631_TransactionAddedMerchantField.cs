using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgeter.Server.Migrations
{
    /// <inheritdoc />
    public partial class TransactionAddedMerchantField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Merchant",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Merchant",
                table: "Transactions");
        }
    }
}
