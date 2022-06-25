using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SteamDeckWindows.Commands;
using SteamDeckWindows.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace SteamDeckWindows
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration.WriteTo.File($"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SteamDeckWindows\\log.txt", rollingInterval: RollingInterval.Day)
                        .WriteTo.Debug()
                        .MinimumLevel.Error()
                        .MinimumLevel.Override("SteamDeckWindows.Commands", Serilog.Events.LogEventLevel.Debug);
                })
                .ConfigureServices(services =>
                {
                    services.AddSingleton<ICommand, MakeSandwichCommand>();
                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            //MainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
