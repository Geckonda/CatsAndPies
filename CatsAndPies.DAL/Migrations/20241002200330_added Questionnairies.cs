using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedQuestionnairies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questionnaire",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "date", nullable: false),
                    Hobby = table.Column<string>(type: "text", nullable: false),
                    Season = table.Column<string>(type: "varchar(100)", nullable: false),
                    Flower = table.Column<string>(type: "varchar(100)", nullable: false),
                    Dish = table.Column<string>(type: "varchar(100)", nullable: false),
                    ChillTime = table.Column<string>(type: "text", nullable: false),
                    Film = table.Column<string>(type: "varchar(100)", nullable: false),
                    Singer = table.Column<string>(type: "varchar(100)", nullable: false),
                    Color = table.Column<string>(type: "varchar(100)", nullable: false),
                    PositiveTraits = table.Column<string>(type: "text", nullable: false),
                    Dream = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questionnaire", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questionnaire_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questionnaire_UserId",
                table: "Questionnaire",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questionnaire");
        }
    }
}
