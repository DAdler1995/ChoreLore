using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreLore.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountStatistics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountStatistics",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TotalGoldEarned = table.Column<int>(type: "int", nullable: false),
                    TotalGoldSpent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountStatistics", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AccountStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountStatistics");
        }
    }
}
