using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmed",
                schema: "reservation",
                table: "tblReservations");

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationStatus",
                schema: "reservation",
                table: "tblReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                schema: "menu",
                table: "tblMenu",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PhotoID",
                schema: "menu",
                table: "tblMenu",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationStatus",
                schema: "reservation",
                table: "tblReservations");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "menu",
                table: "tblMenu");

            migrationBuilder.DropColumn(
                name: "PhotoID",
                schema: "menu",
                table: "tblMenu");

            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmed",
                schema: "reservation",
                table: "tblReservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
