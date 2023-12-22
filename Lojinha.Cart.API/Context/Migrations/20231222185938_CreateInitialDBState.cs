using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lojinha.Cart.API.Context.Migrations
{
    /// <inheritdoc />
    public partial class CreateInitialDBState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    userid = table.Column<string>(name: "user_id", type: "TEXT", nullable: true),
                    finished = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "items",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    name = table.Column<string>(type: "TEXT", nullable: true),
                    price = table.Column<decimal>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    categoryname = table.Column<string>(name: "category_name", type: "TEXT", nullable: true),
                    imageurl = table.Column<string>(name: "image_url", type: "TEXT", nullable: true)
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
                    itemid = table.Column<long>(name: "item_id", type: "INTEGER", nullable: false),
                    couponcode = table.Column<string>(name: "coupon_code", type: "TEXT", nullable: true),
                    quantity = table.Column<long>(type: "INTEGER", nullable: false),
                    cartid = table.Column<long>(name: "cart_id", type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cart_details_carts_cart_id",
                        column: x => x.cartid,
                        principalTable: "carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_details_items_item_id",
                        column: x => x.itemid,
                        principalTable: "items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_details_cart_id",
                table: "cart_details",
                column: "cart_id");

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
                name: "carts");

            migrationBuilder.DropTable(
                name: "items");
        }
    }
}
