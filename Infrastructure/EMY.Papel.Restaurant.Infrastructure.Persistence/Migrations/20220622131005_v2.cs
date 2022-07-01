using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMY.Papel.Restaurant.Infrastructure.Persistence.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderItem_tblOrder_BasketID",
                schema: "order",
                table: "tblOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_tblOrderItem_BasketID",
                schema: "order",
                table: "tblOrderItem");

            migrationBuilder.DropColumn(
                name: "BasketID",
                schema: "order",
                table: "tblOrderItem");

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderItem_OrderID",
                schema: "order",
                table: "tblOrderItem",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderItem_tblOrder_OrderID",
                schema: "order",
                table: "tblOrderItem",
                column: "OrderID",
                principalSchema: "order",
                principalTable: "tblOrder",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblOrderItem_tblOrder_OrderID",
                schema: "order",
                table: "tblOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_tblOrderItem_OrderID",
                schema: "order",
                table: "tblOrderItem");

            migrationBuilder.AddColumn<Guid>(
                name: "BasketID",
                schema: "order",
                table: "tblOrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tblOrderItem_BasketID",
                schema: "order",
                table: "tblOrderItem",
                column: "BasketID");

            migrationBuilder.AddForeignKey(
                name: "FK_tblOrderItem_tblOrder_BasketID",
                schema: "order",
                table: "tblOrderItem",
                column: "BasketID",
                principalSchema: "order",
                principalTable: "tblOrder",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
