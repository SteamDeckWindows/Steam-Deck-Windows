using SteamDeckWindows.Services;
using System.Threading.Tasks;
using System.Windows;
using AutoUpdaterDotNET;

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
        }

        private async void DownLoadDrivers_Click(object sender, RoutedEventArgs e)
        {
            Task task = new DriverService().DownloadDrivers(ProgressBar, SubProgressBar, ProgressLabel, SubProgressLabel);
            await task;
        }
    }
}
