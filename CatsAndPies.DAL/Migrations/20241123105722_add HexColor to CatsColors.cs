using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addHexColortoCatsColors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HexColor",
                table: "CatsColors",
                type: "varchar(6)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "CatsColors",
                keyColumn: "Id",
                keyValue: 1,
                column: "HexColor",
                value: "FFFFFF");

            migrationBuilder.UpdateData(
                table: "CatsColors",
                keyColumn: "Id",
                keyValue: 2,
                column: "HexColor",
                value: "000000");

            migrationBuilder.UpdateData(
                table: "CatsColors",
                keyColumn: "Id",
                keyValue: 3,
                column: "HexColor",
                value: "D2691E");

            migrationBuilder.UpdateData(
                table: "CatsColors",
                keyColumn: "Id",
                keyValue: 4,
                column: "HexColor",
                value: "A9A9A9");

            migrationBuilder.UpdateData(
                table: "CatsColors",
                keyColumn: "Id",
                keyValue: 5,
                column: "HexColor",
                value: "7B3F00");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HexColor",
                table: "CatsColors");
        }
    }
}
