using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace RelationalDatabases.Migrations
{
    public partial class AddedPets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "caretakers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
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
                name: "vets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cvr = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true),
                    phone = table.Column<string>(type: "text", nullable: true),
                    addressId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vets_addresses_addressId",
                        column: x => x.addressId,
                        principalTable: "addresses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    age = table.Column<int>(type: "integer", nullable: false),
                    vetId = table.Column<long>(type: "bigint", nullable: true),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    lifeCount = table.Column<int>(type: "integer", nullable: true),
                    barkPitch = table.Column<string>(type: "text", nullable: true)
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

            migrationBuilder.CreateIndex(
                name: "IX_CaretakerPet_petsId",
                table: "CaretakerPet",
                column: "petsId");

            migrationBuilder.CreateIndex(
                name: "IX_caretakers_addressId",
                table: "caretakers",
                column: "addressId");

            migrationBuilder.CreateIndex(
                name: "IX_pets_vetId",
                table: "pets",
                column: "vetId");

            migrationBuilder.CreateIndex(
                name: "IX_vets_addressId",
                table: "vets",
                column: "addressId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaretakerPet");

            migrationBuilder.DropTable(
                name: "caretakers");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "vets");
        }
    }
}
