using Microsoft.EntityFrameworkCore.Migrations;

namespace Fullstack.Data.Migrations
{
    public partial class advertDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AdvertDetails",
                table: "Adverts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdvertDetails",
                table: "Adverts");
        }
    }
}
