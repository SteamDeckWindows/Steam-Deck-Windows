using Microsoft.EntityFrameworkCore;
using SteamDeckWindows.Models;
using System;

namespace SteamDeckWindows
{
    public class DatabaseContext : DbContext
    {
        private readonly string databaseFullPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\database.db";
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ToolSetting> ToolSettings { get; set; }
        public DbSet<EmulatorSetting> EmulatorSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={databaseFullPath}");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}
