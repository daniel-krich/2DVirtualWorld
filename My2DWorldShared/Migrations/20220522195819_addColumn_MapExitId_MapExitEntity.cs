using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace My2DWorldShared.Migrations
{
    public partial class addColumn_MapExitId_MapExitEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MapExitId",
                table: "MapExits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MapExits_MapExitId",
                table: "MapExits",
                column: "MapExitId");

            migrationBuilder.AddForeignKey(
                name: "FK_MapExits_Maps_MapExitId",
                table: "MapExits",
                column: "MapExitId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MapExits_Maps_MapExitId",
                table: "MapExits");

            migrationBuilder.DropIndex(
                name: "IX_MapExits_MapExitId",
                table: "MapExits");

            migrationBuilder.DropColumn(
                name: "MapExitId",
                table: "MapExits");
        }
    }
}
