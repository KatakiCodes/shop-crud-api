using Microsoft.EntityFrameworkCore;
using Shop_API_Baltaio.Models;

namespace Shop_API_Baltaio.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {    
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories {get; set;}
        public DbSet<Product> Products {get; set;}
    }
}