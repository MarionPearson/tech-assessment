using Microsoft.EntityFrameworkCore;

namespace SogetiAssessment.Contexts
{
    public interface IContextWrapper<T> where T : class
    {
        public DbContext Context { get; set; }
        public DbSet<T> PrimarySet { get; }
    }
}
