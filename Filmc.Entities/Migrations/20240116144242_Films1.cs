using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HideName = table.Column<string>(type: "TEXT", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookGenres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    HideName = table.Column<string>(type: "TEXT", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmGenres",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    IsSerial = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FilmTags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    GenreId = table.Column<long>(type: "INTEGER", nullable: false),
                    PublicationYear = table.Column<long>(type: "INTEGER", nullable: true),
                    IsReaded = table.Column<int>(type: "INTEGER", nullable: false),
                    StartReadDate = table.Column<string>(type: "TEXT", nullable: true),
                    EndReadDate = table.Column<string>(type: "TEXT", nullable: true),
                    Mark = table.Column<int>(type: "INTEGER", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    CountOfReadings = table.Column<long>(type: "INTEGER", nullable: true),
                    Bookmark = table.Column<string>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: true),
                    CategoryListId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsOnTheBlacklist = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOnSecretMode = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "BookCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Books_BookGenres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "BookGenres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    GenreId = table.Column<long>(type: "INTEGER", nullable: false),
                    RealiseYear = table.Column<long>(type: "INTEGER", nullable: true),
                    IsWatched = table.Column<int>(type: "INTEGER", nullable: false),
                    Mark = table.Column<int>(type: "INTEGER", nullable: true),
                    EndWatchDate = table.Column<string>(type: "TEXT", nullable: true),
                    Comment = table.Column<string>(type: "TEXT", nullable: false),
                    CountOfViews = table.Column<long>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<long>(type: "INTEGER", nullable: true),
                    CategoryListId = table.Column<long>(type: "INTEGER", nullable: true),
                    IsOnTheBlacklist = table.Column<int>(type: "INTEGER", nullable: false),
                    IsOnSecretMode = table.Column<int>(type: "INTEGER", nullable: false),
                    StartWatchDate = table.Column<string>(type: "TEXT", nullable: true),
                    WatchedSeries = table.Column<long>(type: "INTEGER", nullable: true),
                    TotalSeries = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Films_FilmCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "FilmCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Films_FilmGenres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "FilmGenres",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookHasTags",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    TagId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookHasTags", x => new { x.BookId, x.TagId });
                    table.ForeignKey(
                        name: "FK_BookHasTags_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookHasTags_BookTags_TagId",
                        column: x => x.TagId,
                        principalTable: "BookTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BooksInPriority",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksInPriority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksInPriority_Books_Id",
                        column: x => x.Id,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BookSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BookId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookSources_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilmHasTags",
                columns: table => new
                {
                    FilmId = table.Column<long>(type: "INTEGER", nullable: false),
                    TagId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmHasTags", x => new { x.FilmId, x.TagId });
                    table.ForeignKey(
                        name: "FK_FilmHasTags_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FilmHasTags_FilmTags_TagId",
                        column: x => x.TagId,
                        principalTable: "FilmTags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilmsInPriority",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false),
                    CreationDate = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmsInPriority", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmsInPriority_Films_Id",
                        column: x => x.Id,
                        principalTable: "Films",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FilmSources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FilmId = table.Column<long>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmSources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FilmSources_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCategories_Id",
                table: "BookCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookGenres_Id",
                table: "BookGenres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookHasTags_TagId",
                table: "BookHasTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryId",
                table: "Books",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_GenreId",
                table: "Books",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_Id",
                table: "Books",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BooksInPriority_Id",
                table: "BooksInPriority",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookSources_BookId",
                table: "BookSources",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookSources_Id",
                table: "BookSources",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookTags_Id",
                table: "BookTags",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmCategories_Id",
                table: "FilmCategories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenres_Id",
                table: "FilmGenres",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmHasTags_TagId",
                table: "FilmHasTags",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_CategoryId",
                table: "Films",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_GenreId",
                table: "Films",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Films_Id",
                table: "Films",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmsInPriority_Id",
                table: "FilmsInPriority",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmSources_FilmId",
                table: "FilmSources",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmSources_Id",
                table: "FilmSources",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilmTags_Id",
                table: "FilmTags",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookHasTags");

            migrationBuilder.DropTable(
                name: "BooksInPriority");

            migrationBuilder.DropTable(
                name: "BookSources");

            migrationBuilder.DropTable(
                name: "FilmHasTags");

            migrationBuilder.DropTable(
                name: "FilmsInPriority");

            migrationBuilder.DropTable(
                name: "FilmSources");

            migrationBuilder.DropTable(
                name: "BookTags");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "FilmTags");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "BookCategories");

            migrationBuilder.DropTable(
                name: "BookGenres");

            migrationBuilder.DropTable(
                name: "FilmCategories");

            migrationBuilder.DropTable(
                name: "FilmGenres");
        }
    }
}
