using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRepairEnums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "PropertyRepairs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "PropertyRepairs");

            migrationBuilder.AddColumn<int>(
                name: "RepairStatus",
                table: "PropertyRepairs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RepairType",
                table: "PropertyRepairs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RepairStatus",
                table: "PropertyRepairs");

            migrationBuilder.DropColumn(
                name: "RepairType",
                table: "PropertyRepairs");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "PropertyRepairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "PropertyRepairs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
