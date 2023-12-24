using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lojinha.Order.API.Context.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Finished = table.Column<bool>(type: "INTEGER", nullable: false),
                    total = table.Column<decimal>(type: "TEXT", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "TEXT", nullable: true),
                    lastname = table.Column<string>(name: "last_name", type: "TEXT", nullable: true),
                    purchasedate = table.Column<DateTime>(name: "purchase_date", type: "TEXT", nullable: false),
                    phone = table.Column<string>(type: "TEXT", nullable: true),
                    email = table.Column<string>(type: "TEXT", nullable: true),
                    cardnumber = table.Column<string>(name: "card_number", type: "TEXT", nullable: true),
                    cvv = table.Column<string>(type: "TEXT", nullable: true),
                    expirymonthyear = table.Column<string>(name: "expiry_month_year", type: "TEXT", nullable: true),
                    status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    itemid = table.Column<long>(name: "item_id", type: "INTEGER", nullable: false),
                    productname = table.Column<string>(name: "product_name", type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    couponcode = table.Column<string>(name: "coupon_code", type: "TEXT", nullable: true),
                    Quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    OrderEntityId = table.Column<long>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_details_orders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_details_OrderEntityId",
                table: "order_details",
                column: "OrderEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "orders");
        }
    }
}
