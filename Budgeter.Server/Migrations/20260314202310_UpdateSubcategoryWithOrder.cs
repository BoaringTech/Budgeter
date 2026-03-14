using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Budgeter.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubcategoryWithOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CateogryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_CateogryId",
                table: "Subcategories");

            migrationBuilder.RenameColumn(
                name: "CateogryId",
                table: "Subcategories",
                newName: "Order");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Subcategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subcategories_Categories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropIndex(
                name: "IX_Subcategories_CategoryId",
                table: "Subcategories");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Subcategories");

            migrationBuilder.RenameColumn(
                name: "Order",
                table: "Subcategories",
                newName: "CateogryId");

            migrationBuilder.CreateIndex(
                name: "IX_Subcategories_CateogryId",
                table: "Subcategories",
                column: "CateogryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subcategories_Categories_CateogryId",
                table: "Subcategories",
                column: "CateogryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
