using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "HeaderPhotoID",
                schema: "menu",
                table: "tblMenuCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "HeaderPhotoURL",
                schema: "menu",
                table: "tblMenuCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "LogoPhotoID",
                schema: "menu",
                table: "tblMenuCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "LogoPhotoURL",
                schema: "menu",
                table: "tblMenuCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeaderPhotoID",
                schema: "menu",
                table: "tblMenuCategory");

            migrationBuilder.DropColumn(
                name: "HeaderPhotoURL",
                schema: "menu",
                table: "tblMenuCategory");

            migrationBuilder.DropColumn(
                name: "LogoPhotoID",
                schema: "menu",
                table: "tblMenuCategory");

            migrationBuilder.DropColumn(
                name: "LogoPhotoURL",
                schema: "menu",
                table: "tblMenuCategory");
        }
    }
}
