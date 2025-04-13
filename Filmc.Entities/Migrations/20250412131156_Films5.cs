using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Filmc.Entities.Migrations
{
    public partial class Films5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IndexInList",
                table: "FilmSources",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IndexInList",
                table: "BookSources",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(@"
                WITH OrderedSources AS (
                    SELECT 
                        Id,
                        FilmId,
                        ROW_NUMBER() OVER (PARTITION BY FilmId ORDER BY Id) - 1 AS RowNum
                    FROM FilmSources
                )
                UPDATE FilmSources
                SET IndexInList = (
                    SELECT RowNum
                    FROM OrderedSources
                    WHERE FilmSources.Id = OrderedSources.Id
                )
            ");

            migrationBuilder.Sql(@"
                WITH OrderedSources AS (
                    SELECT 
                        Id,
                        BookId,
                        ROW_NUMBER() OVER (PARTITION BY BookId ORDER BY Id) - 1 AS RowNum
                    FROM BookSources
                )
                UPDATE BookSources
                SET IndexInList = (
                    SELECT RowNum
                    FROM OrderedSources
                    WHERE BookSources.Id = OrderedSources.Id
                )
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IndexInList",
                table: "FilmSources");

            migrationBuilder.DropColumn(
                name: "IndexInList",
                table: "BookSources");
        }
    }
}
