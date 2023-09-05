using DatabaseManager.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseManager
{
    internal class DatabaseContext : DbContext
    {
        public DbSet<GameModel> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename=database.db");
        }
    }
}
