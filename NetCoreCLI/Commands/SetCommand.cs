using System;
using System.IO;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;

namespace NetCoreCLI.Commands
{
    [Command("set", Description = "Set config")]
    public class SetCommand
    {
        private void OnExecute(IConsole console)
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".api-cli");
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var config = new Config
            {
                Endpoint = Prompt.GetString("Specify the endpoint:", "http://localhost:5000/")
            };

            var filePath = Path.Combine(directory, "config.json");

            using (var outputFile = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                outputFile.WriteLine(JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            console.WriteLine($"Config saved in {filePath}.");
        }
    }
}