using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class NullableItemIdInRepairsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyItemId",
                table: "Repairs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs",
                column: "PropertyItemId",
                principalTable: "PropertyItems",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyItems_PropertyItemId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyItemId",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                principalColumn: "Id");
        }
    }
}
