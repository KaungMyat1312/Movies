using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCMovie.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSaveAndFavouriteTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourite_AspNetUsers_UserId",
                table: "UserFavourite");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourite_Movies_MovieId",
                table: "UserFavourite");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSave_AspNetUsers_UserId",
                table: "UserSave");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSave_Movies_MovieId",
                table: "UserSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSave",
                table: "UserSave");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavourite",
                table: "UserFavourite");

            migrationBuilder.RenameTable(
                name: "UserSave",
                newName: "UserSaves");

            migrationBuilder.RenameTable(
                name: "UserFavourite",
                newName: "UserFavourites");

            migrationBuilder.RenameIndex(
                name: "IX_UserSave_MovieId",
                table: "UserSaves",
                newName: "IX_UserSaves_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavourite_MovieId",
                table: "UserFavourites",
                newName: "IX_UserFavourites_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSaves",
                table: "UserSaves",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavourites",
                table: "UserFavourites",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourites_AspNetUsers_UserId",
                table: "UserFavourites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourites_Movies_MovieId",
                table: "UserFavourites",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSaves_AspNetUsers_UserId",
                table: "UserSaves",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSaves_Movies_MovieId",
                table: "UserSaves",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourites_AspNetUsers_UserId",
                table: "UserFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserFavourites_Movies_MovieId",
                table: "UserFavourites");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaves_AspNetUsers_UserId",
                table: "UserSaves");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSaves_Movies_MovieId",
                table: "UserSaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSaves",
                table: "UserSaves");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserFavourites",
                table: "UserFavourites");

            migrationBuilder.RenameTable(
                name: "UserSaves",
                newName: "UserSave");

            migrationBuilder.RenameTable(
                name: "UserFavourites",
                newName: "UserFavourite");

            migrationBuilder.RenameIndex(
                name: "IX_UserSaves_MovieId",
                table: "UserSave",
                newName: "IX_UserSave_MovieId");

            migrationBuilder.RenameIndex(
                name: "IX_UserFavourites_MovieId",
                table: "UserFavourite",
                newName: "IX_UserFavourite_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSave",
                table: "UserSave",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserFavourite",
                table: "UserFavourite",
                columns: new[] { "UserId", "MovieId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourite_AspNetUsers_UserId",
                table: "UserFavourite",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserFavourite_Movies_MovieId",
                table: "UserFavourite",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSave_AspNetUsers_UserId",
                table: "UserSave",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSave_Movies_MovieId",
                table: "UserSave",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
