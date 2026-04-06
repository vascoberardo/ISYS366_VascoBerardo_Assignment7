using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISYS366_VascoBerardo_Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class AddRankToMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Movie",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Movie");
        }
    }
}
