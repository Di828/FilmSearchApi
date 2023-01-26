using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmSearch.Migrations
{
    /// <inheritdoc />
    public partial class FilmReviewRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Films_ReviewedFilmId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewedFilmId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewedFilmId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_FilmId",
                table: "Reviews",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Films_FilmId",
                table: "Reviews",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Films_FilmId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_FilmId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewedFilmId",
                table: "Reviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewedFilmId",
                table: "Reviews",
                column: "ReviewedFilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Films_ReviewedFilmId",
                table: "Reviews",
                column: "ReviewedFilmId",
                principalTable: "Films",
                principalColumn: "Id");
        }
    }
}
