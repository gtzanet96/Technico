using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class Remove_property_owner_id_from_repairs_table_part2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertiesId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.RenameColumn(
                name: "PropertiesId",
                table: "PropertyItemPropertyOwner",
                newName: "PropertyItemsId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertyItemsId",
                table: "PropertyItemPropertyOwner",
                column: "PropertyItemsId",
                principalTable: "PropertyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertyItemsId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.RenameColumn(
                name: "PropertyItemsId",
                table: "PropertyItemPropertyOwner",
                newName: "PropertiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertiesId",
                table: "PropertyItemPropertyOwner",
                column: "PropertiesId",
                principalTable: "PropertyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
