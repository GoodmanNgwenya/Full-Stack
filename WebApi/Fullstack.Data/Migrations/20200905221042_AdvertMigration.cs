using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fullstack.Data.Migrations
{
    public partial class AdvertMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adverts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    advertHeadlineText = table.Column<string>(nullable: true),
                    province = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    releaseDate = table.Column<DateTime>(nullable: false),
                    imageUrl = table.Column<byte[]>(nullable: true),
                    advertStatus = table.Column<string>(nullable: true),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adverts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adverts_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_userId",
                table: "Adverts",
                column: "userId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adverts");
        }
    }
}
