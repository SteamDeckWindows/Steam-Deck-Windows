﻿using Microsoft.Extensions.Logging;
using System.Windows;

namespace SteamDeckWindows.Commands
{
    public class MakeSandwichCommand : CommandBase
    {
        private readonly ILogger<MakeSandwichCommand> _makeSandwichCommandLogger;

        public MakeSandwichCommand(ILogger<MakeSandwichCommand> makeSandwichCommandLogger)
        {
            _makeSandwichCommandLogger = makeSandwichCommandLogger;
        }

        public override void Execute(object parameter)
        {
            _makeSandwichCommandLogger.LogInformation("Creating a sandwich.");

            MessageBox.Show("Successfully made sandwich.", "Done", MessageBoxButton.OK);

            _makeSandwichCommandLogger.LogError("Failed to create a sandwich.");
        }
    }
}
