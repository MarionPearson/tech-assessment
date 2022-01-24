using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Models;

namespace SogetiAssessment.Contexts
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
        }

        public DbSet<Order> PrimarySet { get; set; } = null!;
    }
}
