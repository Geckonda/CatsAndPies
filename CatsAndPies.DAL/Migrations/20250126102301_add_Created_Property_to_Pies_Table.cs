using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class add_Created_Property_to_Pies_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Pies",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Pies");
        }
    }
}
