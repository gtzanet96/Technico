using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Technico.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PropertyItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyIdentificationNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    YearOfConstruction = table.Column<int>(type: "int", nullable: false),
                    PropertyType = table.Column<int>(type: "int", nullable: false),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyOwners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VAT = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                    table.PrimaryKey("PK_PropertyOwners", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyItemPropertyOwner",
                columns: table => new
                {
                    PropertyItemsId = table.Column<int>(type: "int", nullable: false),
                    PropertyOwnersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyItemPropertyOwner", x => new { x.PropertyItemsId, x.PropertyOwnersId });
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyItems_PropertyItemsId",
                        column: x => x.PropertyItemsId,
                        principalTable: "PropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PropertyItemPropertyOwner_PropertyOwners_PropertyOwnersId",
                        column: x => x.PropertyOwnersId,
                        principalTable: "PropertyOwners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PropertyRepairs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduledDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RepairDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    IsDeactivated = table.Column<bool>(type: "bit", nullable: false),
                    PropertyItemId = table.Column<int>(type: "int", nullable: true),
                    PropertyOwnerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PropertyRepairs_PropertyItems_PropertyItemId",
                        column: x => x.PropertyItemId,
                        principalTable: "PropertyItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_PropertyRepairs_PropertyOwners_PropertyOwnerId",
                        column: x => x.PropertyOwnerId,
                        principalTable: "PropertyOwners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PropertyItemPropertyOwner_PropertyOwnersId",
                table: "PropertyItemPropertyOwner",
                column: "PropertyOwnersId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyItems_PropertyIdentificationNumber",
                table: "PropertyItems",
                column: "PropertyIdentificationNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyOwners_VAT",
                table: "PropertyOwners",
                column: "VAT",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRepairs_PropertyItemId",
                table: "PropertyRepairs",
                column: "PropertyItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PropertyRepairs_PropertyOwnerId",
                table: "PropertyRepairs",
                column: "PropertyOwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PropertyItemPropertyOwner");

            migrationBuilder.DropTable(
                name: "PropertyRepairs");

            migrationBuilder.DropTable(
                name: "PropertyItems");

            migrationBuilder.DropTable(
                name: "PropertyOwners");
        }
    }
}
