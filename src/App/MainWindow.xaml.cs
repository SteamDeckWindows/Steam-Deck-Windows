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
            AutoUpdater.Start("https://github.com/SteamDeckWindows/Steam-Deck-Windows/blob/dev/src/App/Latest.xml");
        }

        private async void DownLoadDrivers_Click(object sender, RoutedEventArgs e)
        {
            Task task = new DriverService().DownloadDrivers(ProgressBar, SubProgressBar, ProgressLabel, SubProgressLabel);
            await task;
        }
    }
}
