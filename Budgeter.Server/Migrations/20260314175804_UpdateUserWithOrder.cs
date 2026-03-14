using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgeter.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserWithOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Users");
        }
    }
}
