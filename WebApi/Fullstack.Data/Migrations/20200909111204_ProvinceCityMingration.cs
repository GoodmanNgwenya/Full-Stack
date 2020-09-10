using Microsoft.EntityFrameworkCore.Migrations;

namespace Fullstack.Data.Migrations
{
    public partial class ProvinceCityMingration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_EntityProvinceId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_EntityProvinceId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "EntityProvinceId",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "ProvinceName",
                table: "Provinces",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CityName",
                table: "Cities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities",
                column: "ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities",
                column: "ProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Provinces_ProvinceId",
                table: "Cities");

            migrationBuilder.DropIndex(
                name: "IX_Cities_ProvinceId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "ProvinceName",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "CityName",
                table: "Cities");

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "Provinces",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EntityProvinceId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cities_EntityProvinceId",
                table: "Cities",
                column: "EntityProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Provinces_EntityProvinceId",
                table: "Cities",
                column: "EntityProvinceId",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
