using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FilmCategoryId",
                table: "Films",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookCategoryId",
                table: "Books",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Films_FilmCategoryId",
                table: "Films",
                column: "FilmCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_BookCategoryId",
                table: "Books",
                column: "BookCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookCategories_BookCategoryId",
                table: "Books",
                column: "BookCategoryId",
                principalTable: "BookCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Films_FilmCategories_FilmCategoryId",
                table: "Films",
                column: "FilmCategoryId",
                principalTable: "FilmCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookCategories_BookCategoryId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Films_FilmCategories_FilmCategoryId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Films_FilmCategoryId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Books_BookCategoryId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "FilmCategoryId",
                table: "Films");

            migrationBuilder.DropColumn(
                name: "BookCategoryId",
                table: "Books");
        }
    }
}
