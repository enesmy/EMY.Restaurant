using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                schema: "order",
                table: "tblOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                schema: "order",
                table: "tblOrder");
        }
    }
}
