using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsWatched",
                table: "Films",
                newName: "WatchProgressId");

            migrationBuilder.RenameColumn(
                name: "IsReaded",
                table: "Books",
                newName: "ReadProgressId");

            migrationBuilder.CreateTable(
                name: "BookReadProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReadProgresses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmWatchProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmWatchProgresses", x => x.Id);
                });

            migrationBuilder.Sql("UPDATE Films SET WatchProgressId = CASE WHEN WatchProgressId = 0 THEN 1 ELSE 2 END");
            migrationBuilder.Sql("UPDATE Books SET ReadProgressId = CASE WHEN ReadProgressId = 0 THEN 1 ELSE 2 END");
            migrationBuilder.Sql("INSERT INTO BookReadProgresses (Id, Name) VALUES (1, \"Not readed\"), (2, \"Readed\");");
            migrationBuilder.Sql("INSERT INTO FilmWatchProgresses (Id, Name) VALUES (1, \"Not watched\"), (2, \"Watched\");");

            migrationBuilder.CreateIndex(
                name: "IX_Films_WatchProgressId",
                table: "Films",
                column: "WatchProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_ReadProgressId",
                table: "Books",
                column: "ReadProgressId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReadProgresses_Id",
                table: "BookReadProgresses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmWatchProgresses_Id",
                table: "FilmWatchProgresses",
                column: "Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookReadProgresses_ReadProgressId",
                table: "Books",
                column: "ReadProgressId",
                principalTable: "BookReadProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Films_FilmWatchProgresses_WatchProgressId",
                table: "Films",
                column: "WatchProgressId",
                principalTable: "FilmWatchProgresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookReadProgresses_ReadProgressId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Films_FilmWatchProgresses_WatchProgressId",
                table: "Films");

            migrationBuilder.DropTable(
                name: "BookReadProgresses");

            migrationBuilder.DropTable(
                name: "FilmWatchProgresses");

            migrationBuilder.DropIndex(
                name: "IX_Films_WatchProgressId",
                table: "Films");

            migrationBuilder.DropIndex(
                name: "IX_Books_ReadProgressId",
                table: "Books");

            migrationBuilder.Sql("UPDATE Films SET WatchProgressId = CASE WHEN WatchProgressId = 1 THEN 0 ELSE 1 END");
            migrationBuilder.Sql("UPDATE Books SET ReadProgressId = CASE WHEN ReadProgressId = 1 THEN 0 ELSE 1 END");

            migrationBuilder.RenameColumn(
                name: "WatchProgressId",
                table: "Films",
                newName: "IsWatched");

            migrationBuilder.RenameColumn(
                name: "ReadProgressId",
                table: "Books",
                newName: "IsReaded");
        }
    }
}
