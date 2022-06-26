using Microsoft.EntityFrameworkCore;
using SteamDeckWindows.Models;
using System;

namespace SteamDeckWindows.Data
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToolSetting>()
              .HasOne(s => s.Setting)
              .WithMany(t => t.Tools)
              .HasForeignKey(fk => fk.SettingId);
            modelBuilder.Entity<EmulatorSetting>()
              .HasOne(s => s.Setting)
              .WithMany(t => t.Emulators)
              .HasForeignKey(fk => fk.SettingId);
        }
    }
}
