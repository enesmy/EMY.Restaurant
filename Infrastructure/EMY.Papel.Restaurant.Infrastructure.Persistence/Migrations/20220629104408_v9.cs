using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPacked",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.RenameColumn(
                name: "AuthorizeStatus",
                schema: "order",
                table: "tblOrder",
                newName: "OrderStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderStatus",
                schema: "order",
                table: "tblOrder",
                newName: "AuthorizeStatus");

            migrationBuilder.AddColumn<bool>(
                name: "IsPacked",
                schema: "order",
                table: "tblOrder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
