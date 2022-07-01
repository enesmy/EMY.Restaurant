using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DestinationTime",
                schema: "order",
                table: "tblOrder",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DestinationTime",
                schema: "order",
                table: "tblOrder");
        }
    }
}
