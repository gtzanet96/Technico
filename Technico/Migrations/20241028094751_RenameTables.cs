using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItem_PropertiesId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyOwner_PropertyOwnersId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Repair_PropertyItem_PropertyItemId",
                table: "Repair");

            migrationBuilder.DropForeignKey(
                name: "FK_Repair_PropertyOwner_PropertyOwnerId",
                table: "Repair");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repair",
                table: "Repair");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyOwner",
                table: "PropertyOwner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyItem",
                table: "PropertyItem");

            migrationBuilder.RenameTable(
                name: "Repair",
                newName: "Repairs");

            migrationBuilder.RenameTable(
                name: "PropertyOwner",
                newName: "PropertyOwners");

            migrationBuilder.RenameTable(
                name: "PropertyItem",
                newName: "PropertyItems");

            migrationBuilder.RenameIndex(
                name: "IX_Repair_PropertyOwnerId",
                table: "Repairs",
                newName: "IX_Repairs_PropertyOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Repair_PropertyItemId",
                table: "Repairs",
                newName: "IX_Repairs_PropertyItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwner_VAT",
                table: "PropertyOwners",
                newName: "IX_PropertyOwners_VAT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyItems",
                table: "PropertyItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertiesId",
                table: "PropertyItemPropertyOwner",
                column: "PropertiesId",
                principalTable: "PropertyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyOwners_PropertyOwnersId",
                table: "PropertyItemPropertyOwner",
                column: "PropertyOwnersId",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs",
                column: "PropertyItemId",
                principalTable: "PropertyItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertiesId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyOwners_PropertyOwnersId",
                table: "PropertyItemPropertyOwner");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyOwners",
                table: "PropertyOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyItems",
                table: "PropertyItems");

            migrationBuilder.RenameTable(
                name: "Repairs",
                newName: "Repair");

            migrationBuilder.RenameTable(
                name: "PropertyOwners",
                newName: "PropertyOwner");

            migrationBuilder.RenameTable(
                name: "PropertyItems",
                newName: "PropertyItem");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_PropertyOwnerId",
                table: "Repair",
                newName: "IX_Repair_PropertyOwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_PropertyItemId",
                table: "Repair",
                newName: "IX_Repair_PropertyItemId");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyOwners_VAT",
                table: "PropertyOwner",
                newName: "IX_PropertyOwner_VAT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repair",
                table: "Repair",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyOwner",
                table: "PropertyOwner",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyItem",
                table: "PropertyItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyItem_PropertiesId",
                table: "PropertyItemPropertyOwner",
                column: "PropertiesId",
                principalTable: "PropertyItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyItemPropertyOwner_PropertyOwner_PropertyOwnersId",
                table: "PropertyItemPropertyOwner",
                column: "PropertyOwnersId",
                principalTable: "PropertyOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repair_PropertyItem_PropertyItemId",
                table: "Repair",
                column: "PropertyItemId",
                principalTable: "PropertyItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repair_PropertyOwner_PropertyOwnerId",
                table: "Repair",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
