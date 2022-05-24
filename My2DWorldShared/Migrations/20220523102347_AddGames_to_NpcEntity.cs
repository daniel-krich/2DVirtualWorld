using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My2DWorldShared.Migrations
{
    public partial class AddGames_to_NpcEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NpcGames_GameId",
                table: "NpcGames",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_NpcGames_Games_GameId",
                table: "NpcGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NpcGames_Games_GameId",
                table: "NpcGames");

            migrationBuilder.DropIndex(
                name: "IX_NpcGames_GameId",
                table: "NpcGames");
        }
    }
}
