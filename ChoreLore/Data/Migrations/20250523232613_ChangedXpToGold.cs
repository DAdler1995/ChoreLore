using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreLore.Migrations
{
    /// <inheritdoc />
    public partial class ChangedXpToGold : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "Chores");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Chores");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "XP",
                table: "Chores",
                newName: "TimesAWeek");

            migrationBuilder.RenameColumn(
                name: "TotalXP",
                table: "AspNetUsers",
                newName: "TotalGold");

            migrationBuilder.AddColumn<int>(
                name: "Gold",
                table: "Chores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gold",
                table: "Chores");

            migrationBuilder.RenameColumn(
                name: "TimesAWeek",
                table: "Chores",
                newName: "XP");

            migrationBuilder.RenameColumn(
                name: "TotalGold",
                table: "AspNetUsers",
                newName: "TotalXP");

            migrationBuilder.AddColumn<string>(
                name: "Frequency",
                table: "Chores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Chores",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
