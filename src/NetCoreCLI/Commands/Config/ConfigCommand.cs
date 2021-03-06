﻿using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Config
{
    [Command("config", Description = "Manage config"),
     Subcommand(typeof(GetCommand), typeof(SetCommand))]
    public class ConfigCommand
    {
        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.Error.WriteLine("Please submit a sub command.");
            app.ShowHelp();
            return 1;
        }
    }
}