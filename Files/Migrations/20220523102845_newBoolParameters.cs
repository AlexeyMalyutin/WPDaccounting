using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class newBoolParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "WPDs");

            migrationBuilder.AddColumn<bool>(
                name: "isPrinted",
                table: "WPDs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isSigned",
                table: "WPDs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isTitlePrinted",
                table: "WPDs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isPrinted",
                table: "WPDs");

            migrationBuilder.DropColumn(
                name: "isSigned",
                table: "WPDs");

            migrationBuilder.DropColumn(
                name: "isTitlePrinted",
                table: "WPDs");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "WPDs",
                type: "datetime2",
                nullable: true);
        }
    }
}
