using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoGameManager.Migrations
{
    /// <inheritdoc />
    public partial class AddPlatformToGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Platform",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Platform",
                table: "Games");
        }
    }
}
