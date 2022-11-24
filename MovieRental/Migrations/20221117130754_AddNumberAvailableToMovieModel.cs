using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieRental.Migrations
{
    public partial class AddNumberAvailableToMovieModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "NumberAvailable",
                table: "Movies",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.Sql("Update Movies SET NumberAvailable = NumberInStock");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberAvailable",
                table: "Movies");
        }
    }
}
