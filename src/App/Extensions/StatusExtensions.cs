using System.Windows.Controls;
namespace SteamDeckWindows.Extensions
{
    public static class StatusExtensions
    {
        public static void Add(TextBox status, string message)
        {
            status.Text += $"{message}\r\n";
            status.ScrollToEnd();
        }
    }
}