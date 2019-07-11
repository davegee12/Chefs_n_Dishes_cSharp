using ChefsAndDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace ChefsAndDishes.Models
{
    public class HomeContext : DbContext
    {
        public HomeContext(DbContextOptions options) : base(options) {}

        public DbSet<Dish> Dishes {get;set;}
        public DbSet<Chef> Chefs {get;set;}
    }
}