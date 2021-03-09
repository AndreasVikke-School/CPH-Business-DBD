using Microsoft.EntityFrameworkCore.Migrations;

namespace VetExercise.Migrations
{
    public partial class inherit_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaretakerPet_Pet_petsId",
                table: "CaretakerPet");

            migrationBuilder.DropForeignKey(
                name: "FK_Cat_Pet_Id",
                table: "Cat");

            migrationBuilder.DropForeignKey(
                name: "FK_Dog_Pet_Id",
                table: "Dog");

            migrationBuilder.DropForeignKey(
                name: "FK_Pet_vets_vetId",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Pet",
                table: "Pet");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dog",
                table: "Dog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cat",
                table: "Cat");

            migrationBuilder.RenameTable(
                name: "Pet",
                newName: "pets");

            migrationBuilder.RenameTable(
                name: "Dog",
                newName: "dogs");

            migrationBuilder.RenameTable(
                name: "Cat",
                newName: "cats");

            migrationBuilder.RenameIndex(
                name: "IX_Pet_vetId",
                table: "pets",
                newName: "IX_pets_vetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pets",
                table: "pets",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_dogs",
                table: "dogs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cats",
                table: "cats",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaretakerPet_pets_petsId",
                table: "CaretakerPet",
                column: "petsId",
                principalTable: "pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_cats_pets_Id",
                table: "cats",
                column: "Id",
                principalTable: "pets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_dogs_pets_Id",
                table: "dogs",
                column: "Id",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CaretakerPet_pets_petsId",
                table: "CaretakerPet");

            migrationBuilder.DropForeignKey(
                name: "FK_cats_pets_Id",
                table: "cats");

            migrationBuilder.DropForeignKey(
                name: "FK_dogs_pets_Id",
                table: "dogs");

            migrationBuilder.DropForeignKey(
                name: "FK_pets_vets_vetId",
                table: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pets",
                table: "pets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_dogs",
                table: "dogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cats",
                table: "cats");

            migrationBuilder.RenameTable(
                name: "pets",
                newName: "Pet");

            migrationBuilder.RenameTable(
                name: "dogs",
                newName: "Dog");

            migrationBuilder.RenameTable(
                name: "cats",
                newName: "Cat");

            migrationBuilder.RenameIndex(
                name: "IX_pets_vetId",
                table: "Pet",
                newName: "IX_Pet_vetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pet",
                table: "Pet",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dog",
                table: "Dog",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cat",
                table: "Cat",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CaretakerPet_Pet_petsId",
                table: "CaretakerPet",
                column: "petsId",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Cat_Pet_Id",
                table: "Cat",
                column: "Id",
                principalTable: "Pet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dog_Pet_Id",
                table: "Dog",
                column: "Id",
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
    }
}
