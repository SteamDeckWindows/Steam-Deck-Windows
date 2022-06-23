using SteamDeckWindows.Services;
using System.Threading.Tasks;
using System.Windows;
using AutoUpdaterDotNET;
using System.Reflection;
using System;

namespace SteamDeckWindows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AutoUpdater.Start("https://steamdeckwindows.github.io/Steam-Deck-Windows/assets/updates/latest.xml");
            GetVersion();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window window = new SettingsWindow();
            window.Owner = this;
            window.ShowDialog();
        }
    }
}
