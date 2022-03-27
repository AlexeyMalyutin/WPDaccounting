using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class PWDModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UploadedBy",
                table: "Files");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfApproval",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfFormalApproval",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Specialization",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subspecialization",
                table: "Files",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Files",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfApproval",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "DateOfFormalApproval",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Specialization",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Subspecialization",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedBy",
                table: "Files",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
