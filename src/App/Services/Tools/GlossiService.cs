using SteamDeckWindows.Clients;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using SteamDeckWindows.Extensions;

namespace SteamDeckWindows.Services.Tools
{
    public class SteamRomManagerService : IToolsService
    {
        public async Task Install(ProgressBar subProgressBar, Label subProgressLabel, string installPath)
        {
            //Todo investigate if we can use appveyyor api to find latest
            var downloadUrl = @"https://ci.appveyor.com/api/buildjobs/4bn7vm501nwecr48/artifacts/x64%2FRelease%2FGlosSI-snapshot.zip";
            await client.DownloadFile(downloadUrl, subProgressBar, $"{installPath}\\Temp\\");
            var filenameWithoutExt = Path.GetFileNameWithoutExtension($"{installPath}\\Temp\\{GlosSI-snapshot.zip}");
            subProgressLabel.Content = $"Unpacking GlosSI-snapshot.zip to {installPath}\\Temp\\{filenameWithoutExt}";
           
            ZipFile.ExtractToDirectory($"{installPath}\\Temp\\GlosSI-snapshot.zip", $"{installPath}\\Temp\\{filenameWithoutExt}", true);
            //move
            MoveDirectory($"{installPath}\\Temp\\{filenameWithoutExt}\\publish", $"{installPath}\\Tools\\GlosSI");
            
            //cleanup
            File.Delete($"{installPath}\\Temp\\{filenameWithoutExt}");

            subProgressLabel.Content = "Finished installing GlosSI";
        }
    }
}
