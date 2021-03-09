using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VetExercise.Migrations
{
    public partial class inherit_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaretakerPet_pets_petsId",
                table: "CaretakerPet");

            migrationBuilder.DropForeignKey(
                name: "FK_pets_vets_vetId",
                table: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pets",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "barkPitch",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "lifeCount",
                table: "pets");

            migrationBuilder.RenameTable(
                name: "pets",
                newName: "Pet");

            migrationBuilder.RenameIndex(
                name: "IX_pets_vetId",
                table: "Pet",
                newName: "IX_Pet_vetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Cat",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lifeCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cat_Pet_Id",
                        column: x => x.Id,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    barkPitch = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dog_Pet_Id",
                        column: x => x.Id,
                        principalTable: "Pet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_CaretakerPet_Pet_petsId",
                table: "CaretakerPet",
                column: "petsId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pet_vets_vetId",
                table: "Pet",
                column: "vetId",
                principalTable: "vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaretakerPet_Pet_petsId",
                table: "CaretakerPet");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_vets_vetId",
                table: "Pet");

            migrationBuilder.DropTable(
                name: "Cat");

            migrationBuilder.DropTable(
                name: "Dog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "pets");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_vetId",
                table: "pets",
                newName: "IX_pets_vetId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_pets",
                table: "pets",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaretakerPet_pets_petsId",
                table: "CaretakerPet",
                column: "petsId",
                principalTable: "pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pets_vets_vetId",
                table: "pets",
                column: "vetId",
                principalTable: "vets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
