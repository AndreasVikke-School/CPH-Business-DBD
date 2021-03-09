using Microsoft.EntityFrameworkCore.Migrations;

namespace VetExercise.Migrations
{
    public partial class bark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BarkPitch",
                table: "dogs",
                newName: "barkPitch");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "barkPitch",
                table: "dogs",
                newName: "BarkPitch");
        }
    }
}
