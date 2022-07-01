using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAuthorized",
                schema: "order",
                table: "tblOrder",
                newName: "AuthorizeStatus");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AuthorizeStatus",
                schema: "order",
                table: "tblOrder",
                newName: "IsAuthorized");
        }
    }
}
