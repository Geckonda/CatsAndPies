using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_Rarity_And_PiesEffect_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EffectId",
                table: "Pies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Rarities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Chance = table.Column<double>(type: "DOUBLE PRECISION", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rarities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PiesEffects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false),
                    RarityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PiesEffects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PiesEffects_Rarities_RarityId",
                        column: x => x.RarityId,
                        principalTable: "Rarities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Rarities",
                columns: new[] { "Id", "Chance", "Name" },
                values: new object[,]
                {
                    { 1, 60.0, "Обычный" },
                    { 2, 25.0, "Необычный" },
                    { 3, 10.0, "Редкий" },
                    { 4, 4.0, "Эпический" },
                    { 5, 0.90000000000000002, "Легендарный" },
                    { 6, 0.10000000000000001, "Мифический" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pies_EffectId",
                table: "Pies",
                column: "EffectId");

            migrationBuilder.CreateIndex(
                name: "IX_PiesEffects_RarityId",
                table: "PiesEffects",
                column: "RarityId");

            migrationBuilder.CreateIndex(
                name: "IX_Rarities_Name",
                table: "Rarities",
                column: "Name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_PiesEffects_EffectId",
                table: "Pies",
                column: "EffectId",
                principalTable: "PiesEffects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_PiesEffects_EffectId",
                table: "Pies");

            migrationBuilder.DropTable(
                name: "PiesEffects");

            migrationBuilder.DropTable(
                name: "Rarities");

            migrationBuilder.DropIndex(
                name: "IX_Pies_EffectId",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "EffectId",
                table: "Pies");
        }
    }
}
