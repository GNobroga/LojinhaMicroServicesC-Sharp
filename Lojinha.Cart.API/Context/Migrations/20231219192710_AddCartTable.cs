﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lojinha.Cart.API.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddCartTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: false),
                    categoryname = table.Column<string>(name: "category_name", type: "TEXT", nullable: false),
                    imageurl = table.Column<string>(name: "image_url", type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cart_details",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: false),
                    couponcode = table.Column<string>(name: "coupon_code", type: "TEXT", nullable: true),
                    itemid = table.Column<long>(name: "item_id", type: "INTEGER", nullable: false),
                    count = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cart_details_items_item_id",
                        column: x => x.itemid,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_item_id",
                table: "cart_details",
                column: "item_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_details");

            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
