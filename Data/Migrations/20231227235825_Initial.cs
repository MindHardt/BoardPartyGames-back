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
                name: "CodenamesGames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayersCount = table.Column<int>(type: "integer", nullable: false),
                    Colors = table.Column<string[]>(type: "text[]", nullable: false),
                    GuessedWords = table.Column<bool[]>(type: "boolean[]", nullable: false),
                    Words = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodenamesGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodenamesPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodenamesPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodenamesWords",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Deck = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodenamesWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpyfallLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    Roles = table.Column<string[]>(type: "text[]", nullable: false),
                    Deck = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SpyfallPlayers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallPlayers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CodenamesGamePlayers",
                columns: table => new
                {
                    CodenamesGameId = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodenamesGamePlayers", x => new { x.CodenamesGameId, x.PlayersId });
                    table.ForeignKey(
                        name: "FK_CodenamesGamePlayers_CodenamesGames_CodenamesGameId",
                        column: x => x.CodenamesGameId,
                        principalTable: "CodenamesGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CodenamesGamePlayers_CodenamesPlayers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "CodenamesPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "SpyfallGamePlayers",
                columns: table => new
                {
                    PlayersId = table.Column<Guid>(type: "uuid", nullable: false),
                    SpyfallGameId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpyfallGamePlayers", x => new { x.PlayersId, x.SpyfallGameId });
                    table.ForeignKey(
                        name: "FK_SpyfallGamePlayers_SpyfallGames_SpyfallGameId",
                        column: x => x.SpyfallGameId,
                        principalTable: "SpyfallGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpyfallGamePlayers_SpyfallPlayers_PlayersId",
                        column: x => x.PlayersId,
                        principalTable: "SpyfallPlayers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CodenamesWords",
                columns: new[] { "Id", "Deck", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { new Guid("106c20c7-58b6-4bb6-bd99-36f4c7bbb347"), "Стандартная", "", "Кафе" },
                    { new Guid("117fbd48-ff40-42e6-b00f-a5f7410b50c4"), "Стандартная", "", "Тюрьма" },
                    { new Guid("128d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "", "Заправка" },
                    { new Guid("136c20c7-58b6-4bb6-bd99-36f4c7bbb34f"), "Стандартная", "", "Кафе" },
                    { new Guid("147fbd48-ff40-42e6-b00f-a5f7410b50c9"), "Стандартная", "", "Тюрьма" },
                    { new Guid("158d22f1-f3c5-42a6-9c7a-9080267bc3ab"), "Стандартная", "", "Заправка" },
                    { new Guid("166c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "Стандартная", "", "Кафе" },
                    { new Guid("177fbd48-ff40-42e6-b00f-a5f7410b50c2"), "Стандартная", "", "Тюрьма" },
                    { new Guid("188d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "", "Заправка" },
                    { new Guid("196c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "Стандартная", "", "Кафе" },
                    { new Guid("207fbd48-ff40-42e6-b00f-a5f7410b50c2"), "Стандартная", "", "Тюрьма" },
                    { new Guid("218d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "", "Заправка" },
                    { new Guid("226c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "Стандартная", "", "Кафе" },
                    { new Guid("237fbd48-ff40-42e6-b00f-a5f7410b50c2"), "Стандартная", "", "Тюрьма" },
                    { new Guid("248d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "", "Заправка" },
                    { new Guid("256c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "Стандартная", "", "Кафе" },
                    { new Guid("487fbd48-fc40-46e6-b10f-a5f7410b50c3"), "Стандартная", "", "Тюрьма" },
                    { new Guid("487fbd48-ff40-42e6-b00f-a5f7410b50c3"), "Стандартная", "", "Тюрьма" },
                    { new Guid("547fcd48-ff40-42e6-d10d-a5f7410b30f1"), "premium gold VIP deck", "", "Поезд" },
                    { new Guid("658d22f1-f3c5-42a6-9c7a-9080267bc3fc"), "Стандартная", "", "Заправка" },
                    { new Guid("736c20c7-58b6-4bb6-bd99-36f4c7bbb322"), "Стандартная", "", "Кафе" },
                    { new Guid("887fbd48-ff40-42e6-b00f-a5f7410b50c3"), "Стандартная", "", "Тюрьма" },
                    { new Guid("958d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "", "Заправка" },
                    { new Guid("e36c20c7-58b6-4bb6-bd99-36f4c7bbb34f"), "Стандартная", "", "Кафе" }
                });

            migrationBuilder.InsertData(
                table: "SpyfallLocations",
                columns: new[] { "Id", "Deck", "ImageUrl", "Name", "Roles" },
                values: new object[,]
                {
                    { new Guid("487fbd48-ff40-42e6-b00f-a5f7410b50c2"), "Стандартная", "img/Spyfall/Тюрьма.jpg", "Тюрьма", new[] { "Охранник", "Повар", "Наркоторговец", "Контрабандист", "Убийца", "Врач", "Вор", "Посетитель" } },
                    { new Guid("547fcd48-ff40-42e6-d10d-a5f7410b30f5"), "premium gold VIP deck", "img/Spyfall/Поезд.jpg", "Поезд", new[] { "Охранник", "Проводник", "Наркоторговец", "Контрабандист", "Убийца", "Повор", "Пассажир" } },
                    { new Guid("558d22f1-f3c5-42a6-9c7a-9080267bc3f3"), "Стандартная", "img/Spyfall/Заправка.jpg", "Заправка", new[] { "Кассир", "Заправщик", "Дальнобойщик", "Водитель", "Уборщица", "Автомойщик", "Ребенок в кресле", "Кот" } },
                    { new Guid("e36c20c7-58b6-4bb6-bd99-36f4c7bbb34b"), "Стандартная", "img/Spyfall/Кафе.jpg", "Кафе", new[] { "Повар", "Официант", "Бармен", "Завсегдатай", "Удаленщик", "Уборщик", "Ребенок", "Толстяк" } }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CodenamesGamePlayers_PlayersId",
                table: "CodenamesGamePlayers",
                column: "PlayersId");

            migrationBuilder.CreateIndex(
                name: "IX_SpyfallGameLocations_SpyfallGameId",
                table: "SpyfallGameLocations",
                column: "SpyfallGameId");

            migrationBuilder.CreateIndex(
                name: "IX_SpyfallGamePlayers_SpyfallGameId",
                table: "SpyfallGamePlayers",
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
                name: "CodenamesGamePlayers");

            migrationBuilder.DropTable(
                name: "CodenamesWords");

            migrationBuilder.DropTable(
                name: "SpyfallGameLocations");

            migrationBuilder.DropTable(
                name: "SpyfallGamePlayers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CodenamesGames");

            migrationBuilder.DropTable(
                name: "CodenamesPlayers");

            migrationBuilder.DropTable(
                name: "SpyfallGames");

            migrationBuilder.DropTable(
                name: "SpyfallPlayers");

            migrationBuilder.DropTable(
                name: "SpyfallLocations");
        }
    }
}
