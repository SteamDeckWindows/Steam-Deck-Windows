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
using Microsoft.Extensions.Logging;

namespace SteamDeckWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// https://docs.microsoft.com/en-us/ef/core/get-started/wpf
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DatabaseContext db = new DatabaseContext();
        public MainWindow()
        {
            InitializeComponent();
        #if DEBUG
        #else
            AutoUpdater.Start("https://raw.githubusercontent.com/SteamDeckWindows/Steam-Deck-Windows/main/docs/assets/updates/latest.xml");
        #endif

            GetVersion();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Run migrations
            db.Database.MigrateAsync();

            // load the entities into EF Core
            db.Settings.Load();

            //TODO Should be replaced by seed methods
            if(db.Settings.FirstOrDefault() == null)
            {
                db.Add(new Setting
                {
                    Name = "Default",
                    SettingId = 1
                });
                db.SaveChanges();
            }

            AddStatus("Welcome to Steam Deck Windows") ;
            AddStatus(db.Settings.First().Name);
            // bind to the source
            //categoryViewSource.Source =
            //    _context.Categories.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            db.Dispose();
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

        private void AddStatus(string text)
        {
            tbStatus.Text += $"{text}\r\n";
        }
    }
}
