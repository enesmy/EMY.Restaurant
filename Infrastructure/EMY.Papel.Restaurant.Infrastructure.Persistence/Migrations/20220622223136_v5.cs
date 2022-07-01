using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPacked",
                schema: "order",
                table: "tblOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                schema: "order",
                table: "tblOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPacked",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "IsSent",
                schema: "order",
                table: "tblOrder");
        }
    }
}
