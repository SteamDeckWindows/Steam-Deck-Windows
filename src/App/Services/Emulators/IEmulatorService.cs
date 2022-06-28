using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services.Emulators
{
    public interface IEmulatorService
    {
        Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath);
    }
}