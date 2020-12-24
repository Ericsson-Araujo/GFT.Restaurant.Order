using GFT.Restaurant.Order.Model;
using Microsoft.EntityFrameworkCore;

namespace GFT.Restaurant.Order.DAL
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {}

        public DbSet<Dish> Dish { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
