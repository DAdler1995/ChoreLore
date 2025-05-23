using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChoreLore.Migrations
{
    /// <inheritdoc />
    public partial class InitialChoreLoreSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Chores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Frequency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    XP = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chores_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChoreCompletions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChoreId = table.Column<int>(type: "int", nullable: true),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChoreCompletions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChoreCompletions_Chores_ChoreId",
                        column: x => x.ChoreId,
                        principalTable: "Chores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChoreCompletions_ChoreId",
                table: "ChoreCompletions",
                column: "ChoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Chores_UserId",
                table: "Chores",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChoreCompletions");

            migrationBuilder.DropTable(
                name: "Chores");
        }
    }
}
