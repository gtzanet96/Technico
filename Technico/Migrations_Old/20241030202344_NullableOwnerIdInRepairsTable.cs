using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class NullableOwnerIdInRepairsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyOwnerId",
                table: "Repairs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyOwnerId",
                table: "Repairs",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
