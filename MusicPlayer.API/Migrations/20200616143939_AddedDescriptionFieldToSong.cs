using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicPlayer.API.Migrations
{
    public partial class AddedDescriptionFieldToSong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Songs",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Songs");
        }
    }
}
