using Microsoft.EntityFrameworkCore;
using TrailAndTailLogBook.Models;

namespace TrailAndTailLogBook.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext()
        {
            SQLitePCL.Batteries_V2.Init();
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "trailtail.db3");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}