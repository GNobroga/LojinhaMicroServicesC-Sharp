using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Lojinha.Coupon.API.Context.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    couponcode = table.Column<string>(name: "coupon_code", type: "TEXT", maxLength: 100, nullable: false),
                    discount = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "id", "coupon_code", "discount" },
                values: new object[,]
                {
                    { 1L, "CUPOM-5", 5m },
                    { 2L, "CUPOM-10", 10m },
                    { 3L, "CUPOM-15", 15m },
                    { 4L, "CUPOM-20", 20m },
                    { 5L, "CUPOM-25", 25m },
                    { 6L, "CUPOM-30", 30m },
                    { 7L, "CUPOM-35", 35m },
                    { 8L, "CUPOM-40", 40m },
                    { 9L, "CUPOM-45", 45m },
                    { 10L, "CUPOM-50", 50m },
                    { 11L, "CUPOM-55", 55m },
                    { 12L, "CUPOM-60", 60m },
                    { 13L, "CUPOM-65", 65m },
                    { 14L, "CUPOM-70", 70m },
                    { 15L, "CUPOM-75", 75m },
                    { 16L, "CUPOM-80", 80m },
                    { 17L, "CUPOM-85", 85m },
                    { 18L, "CUPOM-90", 90m },
                    { 19L, "CUPOM-95", 95m },
                    { 20L, "CUPOM-100", 100m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coupons");
        }
    }
}
