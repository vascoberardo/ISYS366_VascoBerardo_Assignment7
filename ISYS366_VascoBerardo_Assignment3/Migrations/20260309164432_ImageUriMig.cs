using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISYS366_VascoBerardo_Assignment3.Migrations
{
    /// <inheritdoc />
    public partial class ImageUriMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PictureUri",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUri",
                table: "Movie");
        }
    }
}
