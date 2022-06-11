using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserBalance",
                schema: "authorize",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "UserImage",
                schema: "authorize",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "UserToken",
                schema: "authorize",
                table: "tblUsers");

            migrationBuilder.DropColumn(
                name: "UserType",
                schema: "authorize",
                table: "tblUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UserBalance",
                schema: "authorize",
                table: "tblUsers",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "UserImage",
                schema: "authorize",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserToken",
                schema: "authorize",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserType",
                schema: "authorize",
                table: "tblUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
