using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CCV",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "CardHolderName",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "CardNumber",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.DropColumn(
                name: "ExpireMonth",
                schema: "order",
                table: "tblOrder");

            migrationBuilder.RenameColumn(
                name: "ExpireYear",
                schema: "order",
                table: "tblOrder",
                newName: "OrderNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderNumber",
                schema: "order",
                table: "tblOrder",
                newName: "ExpireYear");

            migrationBuilder.AddColumn<string>(
                name: "CCV",
                schema: "order",
                table: "tblOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardHolderName",
                schema: "order",
                table: "tblOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                schema: "order",
                table: "tblOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExpireMonth",
                schema: "order",
                table: "tblOrder",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
