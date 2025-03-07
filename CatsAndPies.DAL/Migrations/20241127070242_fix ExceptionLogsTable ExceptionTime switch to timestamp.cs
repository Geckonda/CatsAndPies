﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatsAndPies.DAL.Migrations
{
    /// <inheritdoc />
    public partial class fixExceptionLogsTableExceptionTimeswitchtotimestamp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExceptionTime",
                table: "ExceptionLogs",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ExceptionTime",
                table: "ExceptionLogs",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");
        }
    }
}
