using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_columnt_ownerId_to_Pies_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Pies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pies_OwnerId",
                table: "Pies",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pies_Users_OwnerId",
                table: "Pies",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pies_Users_OwnerId",
                table: "Pies");

            migrationBuilder.DropIndex(
                name: "IX_Pies_OwnerId",
                table: "Pies");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Pies");
        }
    }
}
