using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "FilmTagCategories");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "BookTagCategories");

            migrationBuilder.AddColumn<byte>(
                name: "ColorA",
                table: "FilmTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorB",
                table: "FilmTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorG",
                table: "FilmTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorR",
                table: "FilmTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorA",
                table: "BookTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorB",
                table: "BookTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorG",
                table: "BookTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<byte>(
                name: "ColorR",
                table: "BookTagCategories",
                type: "INTEGER",
                nullable: false,
                defaultValue: (byte)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorA",
                table: "FilmTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "FilmTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "FilmTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "FilmTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorA",
                table: "BookTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorB",
                table: "BookTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorG",
                table: "BookTagCategories");

            migrationBuilder.DropColumn(
                name: "ColorR",
                table: "BookTagCategories");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "FilmTagCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "BookTagCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
