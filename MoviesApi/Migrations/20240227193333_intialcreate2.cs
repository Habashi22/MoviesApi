using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoviesApi.Migrations
{
    public partial class intialcreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movue_Genre_genreId",
                table: "Movue");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movue",
                table: "Movue");

            migrationBuilder.RenameTable(
                name: "Movue",
                newName: "Movie");

            migrationBuilder.RenameIndex(
                name: "IX_Movue_genreId",
                table: "Movie",
                newName: "IX_Movie_genreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movie",
                table: "Movie",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_Genre_genreId",
                table: "Movie",
                column: "genreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_Genre_genreId",
                table: "Movie");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Movie",
                table: "Movie");

            migrationBuilder.RenameTable(
                name: "Movie",
                newName: "Movue");

            migrationBuilder.RenameIndex(
                name: "IX_Movie_genreId",
                table: "Movue",
                newName: "IX_Movue_genreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Movue",
                table: "Movue",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Movue_Genre_genreId",
                table: "Movue",
                column: "genreId",
                principalTable: "Genre",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
