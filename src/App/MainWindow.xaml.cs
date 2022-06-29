using SteamDeckWindows.Services;
using System.Windows;
using System;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.IO;
using System.Linq;
using SteamDeckWindows.Data;
using AutoUpdaterDotNET;

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
            db.Database.Migrate();

            // load the entities into EF Core
            db.Settings.Load();

            SeedSetting.SeedSettingsData(db,true);

            AddStatus("Welcome to Steam Deck Windows");
            var setting = db.Settings.Include(ts => ts.Tools).Include(es => es.Emulators).First();
            AddStatus($"Files will be installed to {setting.InstallPath}");
            AddStatus($"*************** EMULATORS ***************");
            foreach (var emu in setting.Emulators.ToList())
            {
                AddStatus($"Settings for {emu.Name} Loaded");
            }
            AddStatus($"*************** TOOLS ***************");
            foreach (var tool in setting.Tools.ToList())
            {
                AddStatus($"Settings for {tool.Name} Loaded");
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // clean up database connections
            db.Dispose();
            base.OnClosing(e);
        }

        private async void Update_Click(object sender, RoutedEventArgs e)
        {
            var setting = db.Settings.Include(ts => ts.Tools).Include(es => es.Emulators).FirstOrDefault();

            await new InstallService(tbStatus, ProgressBar, ProgressLabel, SubProgressBar, SubProgressLabel, setting).Update();
            MessageBox.Show("Update completed!");
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
