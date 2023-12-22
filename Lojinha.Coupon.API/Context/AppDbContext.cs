using Microsoft.EntityFrameworkCore;

namespace Lojinha.Coupon.API.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base (options) {}

    public DbSet<Entities.Coupon> Coupons { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        for (int i = 5, currentId = 1; i <= 100 ; i += 5, currentId++)
        {
            modelBuilder.Entity<Entities.Coupon>()
                .HasData(new Entities.Coupon {
                    CouponId = currentId,
                    CouponCode = $"CUPOM-{i}",
                    Discount = i
                });
        }

        base.OnModelCreating(modelBuilder);
    }
}