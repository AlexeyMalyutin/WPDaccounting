using Microsoft.EntityFrameworkCore.Migrations;

namespace Files.Migrations
{
    public partial class Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "WPDs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WPDs_AuthorId",
                table: "WPDs",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_WPDs_Authors_AuthorId",
                table: "WPDs",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WPDs_Authors_AuthorId",
                table: "WPDs");

            migrationBuilder.DropIndex(
                name: "IX_WPDs_AuthorId",
                table: "WPDs");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "WPDs");
        }
    }
}
