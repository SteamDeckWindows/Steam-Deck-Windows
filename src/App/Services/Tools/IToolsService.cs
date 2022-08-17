using SteamDeckWindows.Models;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services.Tools
{
    public interface IToolsService
    {
        Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath, ToolSetting toolSetting, TextBox status);
    }
}