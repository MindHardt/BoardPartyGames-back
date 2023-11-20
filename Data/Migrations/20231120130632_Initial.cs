using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpyfallLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Roles = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpyfallGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayersCount = table.Column<int>(type: "integer", nullable: false),
                    SpiesIndices = table.Column<int[]>(type: "integer[]", nullable: false),
                    LocationId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpyfallGames_SpyfallLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "SpyfallLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SpyfallGameLocations",
                columns: table => new
                {
                    PossibleLocationsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpyfallGameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallGameLocations", x => new { x.PossibleLocationsId, x.SpyfallGameId });
                    table.ForeignKey(
                        name: "FK_SpyfallGameLocations_SpyfallGames_SpyfallGameId",
                        column: x => x.SpyfallGameId,
                        principalTable: "SpyfallGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpyfallGameLocations_SpyfallLocations_PossibleLocationsId",
                        column: x => x.PossibleLocationsId,
                        principalTable: "SpyfallLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "SpyfallLocations",
                columns: new[] { "Id", "ImageUrl", "Name", "Roles" },
                values: new object[,]
                {
                    { new Guid("487fbd48-ff40-42e6-b00f-a5f7410b50c2"), "img/Spyfall/Тюрьма.jpg", "Тюрьма", new[] { "Охранник", "Повар", "Наркоторговец", "Контрабандист", "Убийца", "Врач", "Вор", "Посетитель" } },
                    { new Guid("558d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "img/Spyfall/Заправка.jpg", "Заправка", new[] { "Кассир", "Заправщик", "Дальнобойщик", "Водитель", "Уборщица", "Автомойщик", "Ребенок в кресле", "Кот" } },
                    { new Guid("e36c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "img/Spyfall/Кафе.jpg", "Кафе", new[] { "Повар", "Официант", "Бармен", "Завсегдатай", "Удаленщик", "Уборщик", "Ребенок", "Толстяк" } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpyfallGameLocations_SpyfallGameId",
                table: "SpyfallGameLocations",
                column: "SpyfallGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpyfallGames_LocationId",
                table: "SpyfallGames",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_SpyfallLocations_Name",
                table: "SpyfallLocations",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpyfallGameLocations");

            migrationBuilder.DropTable(
                name: "SpyfallGames");

            migrationBuilder.DropTable(
                name: "SpyfallLocations");
        }
    }
}
