using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyItem",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfConstruction = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VAT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyOwner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyItemPropertyOwner",
                columns: table => new
                {
                    PropertiesId = table.Column<int>(type: "int", nullable: false),
                    PropertyOwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItemPropertyOwner", x => new { x.PropertiesId, x.PropertyOwnersId });
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyItem_PropertiesId",
                        column: x => x.PropertiesId,
                        principalTable: "PropertyItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyOwner_PropertyOwnersId",
                        column: x => x.PropertyOwnersId,
                        principalTable: "PropertyOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    PropertyOwnerId = table.Column<int>(type: "int", nullable: false),
                    PropertyItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repair_PropertyItem_PropertyItemId",
                        column: x => x.PropertyItemId,
                        principalTable: "PropertyItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Repair_PropertyOwner_PropertyOwnerId",
                        column: x => x.PropertyOwnerId,
                        principalTable: "PropertyOwner",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyItemPropertyOwner_PropertyOwnersId",
                table: "PropertyItemPropertyOwner",
                column: "PropertyOwnersId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_PropertyItemId",
                table: "Repair",
                column: "PropertyItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_PropertyOwnerId",
                table: "Repair",
                column: "PropertyOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyItemPropertyOwner");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "PropertyItem");

            migrationBuilder.DropTable(
                name: "PropertyOwner");
        }
    }
}
