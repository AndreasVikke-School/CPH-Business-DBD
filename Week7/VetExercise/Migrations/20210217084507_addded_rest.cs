using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VetExercise.Migrations
{
    public partial class addded_rest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vet_Addresses_addressId",
                table: "Vet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vet",
                table: "Vet");

            migrationBuilder.DropColumn(
                name: "city",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "zip",
                table: "Addresses");

            migrationBuilder.RenameTable(
                name: "Addresses",
                newName: "addresses");

            migrationBuilder.RenameTable(
                name: "Vet",
                newName: "vets");

            migrationBuilder.RenameIndex(
                name: "IX_Vet_addressId",
                table: "vets",
                newName: "IX_vets_addressId");

            migrationBuilder.AddColumn<int>(
                name: "cityzip",
                table: "addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_addresses",
                table: "addresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vets",
                table: "vets",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "caretakers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    addressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caretakers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_caretakers_addresses_addressId",
                        column: x => x.addressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    zip = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.zip);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: false),
                    vetId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_pets_vets_vetId",
                        column: x => x.vetId,
                        principalTable: "vets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaretakerPet",
                columns: table => new
                {
                    caretakersId = table.Column<long>(type: "bigint", nullable: false),
                    petsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaretakerPet", x => new { x.caretakersId, x.petsId });
                    table.ForeignKey(
                        name: "FK_CaretakerPet_caretakers_caretakersId",
                        column: x => x.caretakersId,
                        principalTable: "caretakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaretakerPet_pets_petsId",
                        column: x => x.petsId,
                        principalTable: "pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                    BarkPitch = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_addresses_cityzip",
                table: "addresses",
                column: "cityzip");

            migrationBuilder.CreateIndex(
                name: "IX_CaretakerPet_petsId",
                table: "CaretakerPet",
                column: "petsId");

            migrationBuilder.CreateIndex(
                name: "IX_caretakers_addressId",
                table: "caretakers",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_cats_petId",
                table: "cats",
                column: "petId");

            migrationBuilder.CreateIndex(
                name: "IX_dogs_petId",
                table: "dogs",
                column: "petId");

            migrationBuilder.CreateIndex(
                name: "IX_pets_vetId",
                table: "pets",
                column: "vetId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_cityzip",
                table: "addresses",
                column: "cityzip",
                principalTable: "cities",
                principalColumn: "zip",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vets_addresses_addressId",
                table: "vets",
                column: "addressId",
                principalTable: "addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_cityzip",
                table: "addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_vets_addresses_addressId",
                table: "vets");

            migrationBuilder.DropTable(
                name: "CaretakerPet");

            migrationBuilder.DropTable(
                name: "cats");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "dogs");

            migrationBuilder.DropTable(
                name: "caretakers");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_addresses",
                table: "addresses");

            migrationBuilder.DropIndex(
                name: "IX_addresses_cityzip",
                table: "addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_vets",
                table: "vets");

            migrationBuilder.DropColumn(
                name: "cityzip",
                table: "addresses");

            migrationBuilder.RenameTable(
                name: "addresses",
                newName: "Addresses");

            migrationBuilder.RenameTable(
                name: "vets",
                newName: "Vet");

            migrationBuilder.RenameIndex(
                name: "IX_vets_addressId",
                table: "Vet",
                newName: "IX_Vet_addressId");

            migrationBuilder.AddColumn<string>(
                name: "city",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "zip",
                table: "Addresses",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vet",
                table: "Vet",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vet_Addresses_addressId",
                table: "Vet",
                column: "addressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
