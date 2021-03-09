using Microsoft.EntityFrameworkCore.Migrations;

namespace VetExercise.Migrations
{
    public partial class house_number : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "housenumber",
                table: "addresses",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "housenumber",
                table: "addresses");
        }
    }
}
