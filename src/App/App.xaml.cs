using System;
using System.IO;
using System.Windows;

namespace SteamDeckWindows
{
    public partial class App : Application
    {
        public App()
        {
            // Copy base_database.db if not allready created in user appdata
            //string localAppPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\";
            //Directory.CreateDirectory(localAppPath);
            //if (!File.Exists($"{localAppPath}database.db"))
            //{
            //    var baseDbPath = AppDomain.CurrentDomain.BaseDirectory + "\\base_database.db";
            //    File.Copy(baseDbPath, $"{localAppPath}database.db");
            //}
        }
    }
}
