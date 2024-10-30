using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class deleted_every_owner_repair_relationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PropertyRepairs_PropertyOwners_PropertyOwnerId",
                table: "PropertyRepairs");

            migrationBuilder.DropIndex(
                name: "IX_PropertyRepairs_PropertyOwnerId",
                table: "PropertyRepairs");

            migrationBuilder.DropColumn(
                name: "PropertyOwnerId",
                table: "PropertyRepairs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PropertyOwnerId",
                table: "PropertyRepairs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRepairs_PropertyOwnerId",
                table: "PropertyRepairs",
                column: "PropertyOwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyRepairs_PropertyOwners_PropertyOwnerId",
                table: "PropertyRepairs",
                column: "PropertyOwnerId",
                principalTable: "PropertyOwners",
                principalColumn: "Id");
        }
    }
}
