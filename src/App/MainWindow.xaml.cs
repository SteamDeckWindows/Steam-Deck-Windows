using SteamDeckWindows.Services;
using System.Threading.Tasks;
using System.Windows;
using AutoUpdaterDotNET;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.IO;
using SteamDeckWindows.Models;
using System.Collections.Generic;
using System.Linq;

namespace SteamDeckWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://docs.microsoft.com/en-us/ef/core/get-started/wpf
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        public MainWindow()
        {
            InitializeComponent();
            AutoUpdater.Start("https://raw.githubusercontent.com/SteamDeckWindows/Steam-Deck-Windows/main/docs/assets/updates/latest.xml");
            GetVersion();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Copy base_database.db if not allready created in user appdata
            string databasePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\";
            if (!File.Exists($"{databasePath}database.db"))
            {
                var baseDbPath = AppDomain.CurrentDomain.BaseDirectory + "base_database.db";
                File.Copy(baseDbPath, $"{databasePath}database.db");
            }

            //Run migrations
            _context.Database.MigrateAsync();

            // load the entities into EF Core
            _context.Settings.Load();

            //TODO Should be replaced ny seed methods
            //_context.Add(new Setting{
            //    Name="Default",
            //    SettingId=1
            //});
            //_context.SaveChanges();

            tbStatus.Text = _context.Settings.First().Name;

            // bind to the source
            //categoryViewSource.Source =
            //    _context.Categories.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            _context.Dispose();
            base.OnClosing(e);
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            Task task = new DriverService().DownloadDrivers(ProgressBar, SubProgressBar, ProgressLabel, SubProgressLabel);
            await task;
        }

        private void GetVersion()
        {
            lblVersion.Content = "Version " + GetType().Assembly.GetName().Version;

        }

        private void Settings_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Window window = new SettingsWindow
                {
                    Owner = this
                };
                window.ShowDialog();
            }
            catch(Exception error) {
                MessageBox.Show(error.Message);
            
            }
        }
    }
}
