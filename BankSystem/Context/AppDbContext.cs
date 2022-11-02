using BankSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
    }
}
