using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace NetCoreCLI.Commands
{
    [Command("show", Description = "Show the current config")]
    public class ShowCommand
    {
        private void OnExecute(IConsole console)
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".api-cli", "config.json");

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                try
                {
                    JsonConvert.DeserializeObject<Config>(content);
                    console.WriteLine(content);
                }
                catch
                {
                    console.WriteLine("The config is broken, please use 'config set' command to recreate one.");
                }
            }
            else
            {
                console.WriteLine("Config is not existed, please use 'config set' command to create one.");
            }
        }
    }
}