using SteamDeckWindows.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Forms;

namespace SteamDeckWindows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private readonly DatabaseContext _context = new DatabaseContext();
        private Setting setting { get; set; }
        public SettingsWindow()
        {
            InitializeComponent();
            setting = _context.Settings.First();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnInstallPath_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog
            {
                Description = "Time to select a folder",
                UseDescriptionForTitle = true,
                SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                ShowNewFolderButton = true
            };
            DialogResult result = dialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var path = dialog.SelectedPath;
            }
        }
    }
}
