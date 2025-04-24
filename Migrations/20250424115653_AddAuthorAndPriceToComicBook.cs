using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthorAndPriceToComicBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "ComicBooks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay",
                table: "ComicBooks",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "ComicBooks");

            migrationBuilder.DropColumn(
                name: "PricePerDay",
                table: "ComicBooks");
        }
    }
}
