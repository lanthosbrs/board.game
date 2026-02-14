using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace board.game.GameDb.Migrations
{
    /// <inheritdoc />
    public partial class AddCompleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShelfNumber",
                table: "Games",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShelfNumber",
                table: "Games");
        }
    }
}
