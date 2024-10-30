using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class NullableItemIdInRepairsTablePart2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs",
                column: "PropertyItemId",
                principalTable: "PropertyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs",
                column: "PropertyItemId",
                principalTable: "PropertyItems",
                principalColumn: "Id");
        }
    }
}
