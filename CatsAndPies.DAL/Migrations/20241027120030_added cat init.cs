using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedcatinit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CatsColors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatsColors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CatsPersonalities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CatsPersonalities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ColorId = table.Column<int>(type: "integer", nullable: false),
                    PersonalityId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    AdoptedTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cats_CatsColors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "CatsColors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cats_CatsPersonalities_PersonalityId",
                        column: x => x.PersonalityId,
                        principalTable: "CatsPersonalities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CatsColors",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Белый" },
                    { 2, "Черный" },
                    { 3, "Рыжий" },
                    { 4, "Серый" },
                    { 5, "Шоколадный" }
                });

            migrationBuilder.InsertData(
                table: "CatsPersonalities",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Считает себя лучше всех и не упустит момента показать свое превосходство. Часто саркастичен, но с оттенком высокомерия.", "Заносчивый" },
                    { 2, "Этот кот всегда говорит философским тоном, любит поучать и делиться своей «мудростью». Возможно, его реплики звучат как древние пословицы или загадки.", "Мудрый" },
                    { 3, "Постоянно задает вопросы, интересуется всем подряд, будто бы видит все впервые. В его репликах часто встречаются удивление и восторг.", "Любопытный" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cats_ColorId",
                table: "Cats",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_PersonalityId",
                table: "Cats",
                column: "PersonalityId");

            migrationBuilder.CreateIndex(
                name: "IX_Cats_UserId",
                table: "Cats",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "CatsColors");

            migrationBuilder.DropTable(
                name: "CatsPersonalities");
        }
    }
}
