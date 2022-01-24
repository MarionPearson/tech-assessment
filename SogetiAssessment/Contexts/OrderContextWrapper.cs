using Microsoft.EntityFrameworkCore;
using SogetiAssessment.Models;

namespace SogetiAssessment.Contexts
{
    public class OrderContextWrapper: IContextWrapper<Order>
    {
        public DbContext Context { get; set; }
        public DbSet<Order> PrimarySet
        {
            get { return (Context as OrderContext).PrimarySet; }
        }

        public OrderContextWrapper(DbContextOptions<OrderContext> options)
        {
            Context = new OrderContext(options);
        }
    }
}
