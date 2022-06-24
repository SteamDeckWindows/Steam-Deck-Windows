using Microsoft.EntityFrameworkCore;
using SteamDeckWindows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamDeckWindows
{
    public class DatabaseContext : DbContext
    {
        private readonly string databaseFullPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\database.db";
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={databaseFullPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
