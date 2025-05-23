using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreLore.Migrations
{
    /// <inheritdoc />
    public partial class AddedChoreSimpleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SimpleName",
                table: "Chores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SimpleName",
                table: "Chores");
        }
    }
}
