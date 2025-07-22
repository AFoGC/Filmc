using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "FilmTags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "BookTags",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BookTagCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTagCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmTagCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmTagCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmTags_CategoryId",
                table: "FilmTags",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_CategoryId",
                table: "BookTags",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BookTagCategories_Id",
                table: "BookTagCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmTagCategories_Id",
                table: "FilmTagCategories",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookTags_BookTagCategories_CategoryId",
                table: "BookTags",
                column: "CategoryId",
                principalTable: "BookTagCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmTags_FilmTagCategories_CategoryId",
                table: "FilmTags",
                column: "CategoryId",
                principalTable: "FilmTagCategories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookTags_BookTagCategories_CategoryId",
                table: "BookTags");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmTags_FilmTagCategories_CategoryId",
                table: "FilmTags");

            migrationBuilder.DropTable(
                name: "BookTagCategories");

            migrationBuilder.DropTable(
                name: "FilmTagCategories");

            migrationBuilder.DropIndex(
                name: "IX_FilmTags_CategoryId",
                table: "FilmTags");

            migrationBuilder.DropIndex(
                name: "IX_BookTags_CategoryId",
                table: "BookTags");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "FilmTags");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "BookTags");
        }
    }
}
