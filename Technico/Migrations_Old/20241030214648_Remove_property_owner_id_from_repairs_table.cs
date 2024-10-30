using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class Remove_property_owner_id_from_repairs_table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_PropertyOwners_PropertyOwnerId",
                table: "Repairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
