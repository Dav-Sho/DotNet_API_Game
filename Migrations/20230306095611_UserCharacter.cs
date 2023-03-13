using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace game_rpg.Migrations
{
    /// <inheritdoc />
    public partial class UserCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Characters",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserId",
                table: "Characters",
                newName: "IX_Characters_UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UsersId",
                table: "Characters",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_Users_UsersId",
                table: "Characters");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "Characters",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UsersId",
                table: "Characters",
                newName: "IX_Characters_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_Users_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
