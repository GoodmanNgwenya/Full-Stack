using Microsoft.EntityFrameworkCore.Migrations;

namespace Fullstack.Data.Migrations
{
    public partial class advert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Users_userId",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "userId",
                table: "Adverts",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "releaseDate",
                table: "Adverts",
                newName: "ReleaseDate");

            migrationBuilder.RenameColumn(
                name: "province",
                table: "Adverts",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Adverts",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Adverts",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "city",
                table: "Adverts",
                newName: "City");

            migrationBuilder.RenameColumn(
                name: "advertStatus",
                table: "Adverts",
                newName: "AdvertStatus");

            migrationBuilder.RenameColumn(
                name: "advertHeadlineText",
                table: "Adverts",
                newName: "AdvertHeadlineText");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_userId",
                table: "Adverts",
                newName: "IX_Adverts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Users_UserId",
                table: "Adverts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Users_UserId",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Adverts",
                newName: "userId");

            migrationBuilder.RenameColumn(
                name: "ReleaseDate",
                table: "Adverts",
                newName: "releaseDate");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Adverts",
                newName: "province");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Adverts",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Adverts",
                newName: "imageUrl");

            migrationBuilder.RenameColumn(
                name: "City",
                table: "Adverts",
                newName: "city");

            migrationBuilder.RenameColumn(
                name: "AdvertStatus",
                table: "Adverts",
                newName: "advertStatus");

            migrationBuilder.RenameColumn(
                name: "AdvertHeadlineText",
                table: "Adverts",
                newName: "advertHeadlineText");

            migrationBuilder.RenameIndex(
                name: "IX_Adverts_UserId",
                table: "Adverts",
                newName: "IX_Adverts_userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Users_userId",
                table: "Adverts",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
