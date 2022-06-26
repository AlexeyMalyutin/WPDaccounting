using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class UpdateFormatOfYearField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isTitlePrinted",
                table: "WPDs",
                newName: "IsTitlePrinted");

            migrationBuilder.RenameColumn(
                name: "isSigned",
                table: "WPDs",
                newName: "IsSigned");

            migrationBuilder.RenameColumn(
                name: "isPrinted",
                table: "WPDs",
                newName: "IsPrinted");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "WPDs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsTitlePrinted",
                table: "WPDs",
                newName: "isTitlePrinted");

            migrationBuilder.RenameColumn(
                name: "IsSigned",
                table: "WPDs",
                newName: "isSigned");

            migrationBuilder.RenameColumn(
                name: "IsPrinted",
                table: "WPDs",
                newName: "isPrinted");

            migrationBuilder.AlterColumn<int>(
                name: "Year",
                table: "WPDs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
