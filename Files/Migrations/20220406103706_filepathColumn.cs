using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class filepathColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "WPDs");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "WPDs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "WPDs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileType",
                table: "WPDs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
