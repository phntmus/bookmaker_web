using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookmakerWeb.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EventName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Sport = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.MatchId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    RegDate = table.Column<DateTime>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    BetId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OutCome = table.Column<string>(type: "TEXT", nullable: false),
                    Odds = table.Column<decimal>(type: "TEXT", nullable: false),
                    BetDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    MatchId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.BetId);
                    table.ForeignKey(
                        name: "FK_Bets_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "MatchId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bets_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Matches",
                columns: new[] { "MatchId", "EventName", "Sport", "StartTime" },
                values: new object[,]
                {
                    { 1, "Реал Мадрид — Барселона", "Футбол", new DateTime(2024, 6, 15, 20, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "ЦСКА — Спартак", "Футбол", new DateTime(2024, 6, 16, 18, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Лейкерс — Голден Стэйт", "Баскетбол", new DateTime(2024, 6, 17, 3, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "FullName", "PhoneNumber", "RegDate" },
                values: new object[,]
                {
                    { 1, "ivanov@mail.ru", "Иванов Иван Петрович", "+79001234567", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "petrova@mail.ru", "Петрова Анна Сергеевна", "+79007654321", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "sidorov@mail.ru", "Сидоров Алексей Михайлович", "+79009876543", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Bets",
                columns: new[] { "BetId", "Amount", "BetDate", "MatchId", "Odds", "OutCome", "UserId" },
                values: new object[,]
                {
                    { 1, 1000m, new DateTime(2024, 6, 14, 15, 30, 0, 0, DateTimeKind.Unspecified), 1, 2.10m, "Победа Реала", 1 },
                    { 2, 500m, new DateTime(2024, 6, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, 1.85m, "Тотал больше 2.5", 1 },
                    { 3, 2000m, new DateTime(2024, 6, 16, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, 1.75m, "Победа Lakers", 2 },
                    { 4, 1500m, new DateTime(2024, 6, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 3.20m, "Ничья", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_MatchId",
                table: "Bets",
                column: "MatchId");

            migrationBuilder.CreateIndex(
                name: "IX_Bets_UserId",
                table: "Bets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
