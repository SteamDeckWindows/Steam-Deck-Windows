using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SteamDeckWindows.Services
{
    public interface IDriverService
    {
        Task DownloadDrivers(ProgressBar progressBar, ProgressBar subProgressBar, Label progressLabel, Label subProgressLabel);
    }
}
