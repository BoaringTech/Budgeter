using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Budgeter.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BooleanSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooleanSettings", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BooleanSettings",
                columns: new[] { "Id", "Enabled", "Name" },
                values: new object[,]
                {
                    { 1, true, "TrackAccounts" },
                    { 2, true, "TrackUsers" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooleanSettings");
        }
    }
}
