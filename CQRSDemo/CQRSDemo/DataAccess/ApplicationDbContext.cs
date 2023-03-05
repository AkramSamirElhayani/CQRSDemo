using CQRSDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CQRSDemo.DataAccess
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
            this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Order>()
                .HasMany (o=>o.OrderItems)
                .WithOne(oi=>oi.Order)
                .HasForeignKey(x=>x.OrderId);

            modelBuilder.Entity<OrderItem>().Property(o=>o.Price).HasColumnType("decimal(16,4)");
        }

        public virtual DbSet<Order>  Orders { get; set; }
        public virtual DbSet<OrderItem>  OrderItems { get; set; }
    }
}
