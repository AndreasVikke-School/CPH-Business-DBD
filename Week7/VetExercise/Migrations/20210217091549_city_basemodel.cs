using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace VetExercise.Migrations
{
    public partial class city_basemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_cityzip",
                table: "addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cities",
                table: "cities");

            migrationBuilder.DropIndex(
                name: "IX_addresses_cityzip",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "cityzip",
                table: "addresses");

            migrationBuilder.AlterColumn<int>(
                name: "zip",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "Id",
                table: "cities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<long>(
                name: "cityId",
                table: "addresses",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cities",
                table: "cities",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_cityId",
                table: "addresses",
                column: "cityId");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_cityId",
                table: "addresses",
                column: "cityId",
                principalTable: "cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_addresses_cities_cityId",
                table: "addresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cities",
                table: "cities");

            migrationBuilder.DropIndex(
                name: "IX_addresses_cityId",
                table: "addresses");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "cities");

            migrationBuilder.DropColumn(
                name: "cityId",
                table: "addresses");

            migrationBuilder.AlterColumn<int>(
                name: "zip",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "cityzip",
                table: "addresses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_cities",
                table: "cities",
                column: "zip");

            migrationBuilder.CreateIndex(
                name: "IX_addresses_cityzip",
                table: "addresses",
                column: "cityzip");

            migrationBuilder.AddForeignKey(
                name: "FK_addresses_cities_cityzip",
                table: "addresses",
                column: "cityzip",
                principalTable: "cities",
                principalColumn: "zip",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
