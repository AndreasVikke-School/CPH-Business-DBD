using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VetExercise.Migrations
{
    public partial class inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cats");

            migrationBuilder.DropTable(
                name: "dogs");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "pets",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "barkPitch",
                table: "pets",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "lifeCount",
                table: "pets",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "barkPitch",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "lifeCount",
                table: "pets");

            migrationBuilder.CreateTable(
                name: "cats",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lifeCount = table.Column<int>(type: "integer", nullable: false),
                    petId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cats_pets_petId",
                        column: x => x.petId,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "dogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barkPitch = table.Column<int>(type: "integer", nullable: false),
                    petId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dogs_pets_petId",
                        column: x => x.petId,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cats_petId",
                table: "cats",
                column: "petId");

            migrationBuilder.CreateIndex(
                name: "IX_dogs_petId",
                table: "dogs",
                column: "petId");
        }
    }
}
