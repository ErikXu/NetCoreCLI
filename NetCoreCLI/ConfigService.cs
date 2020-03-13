using System;
using System.IO;
using System.Text;
using McMaster.Extensions.CommandLineUtils;
using NetCoreCLI.Models;
using Newtonsoft.Json;

namespace NetCoreCLI
{
    public interface IConfigService
    {
        void Set();

        Config Get();
    }

    public class ConfigService: IConfigService
    {
        private readonly IConsole _console;
        private readonly string _directoryName;
        private readonly string _fileName;

        public ConfigService(IConsole console)
        {
            _console = console;
            _directoryName = ".api-cli";
            _fileName = "config.json";
        }

        public void Set()
        {
            var directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _directoryName);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var config = new Config
            {
                Endpoint = Prompt.GetString("Specify the endpoint:", "http://localhost:5000/")
            };

            if (!config.Endpoint.EndsWith("/"))
            {
                config.Endpoint += "/";
            }

            var filePath = Path.Combine(directory, _fileName);

            using (var outputFile = new StreamWriter(filePath, false, Encoding.UTF8))
            {
                outputFile.WriteLine(JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            _console.WriteLine($"Config saved in {filePath}.");
        }

        public Config Get()
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), _directoryName, _fileName);

            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);
                try
                {
                    var config = JsonConvert.DeserializeObject<Config>(content);
                    return config;
                }
                catch
                {
                    _console.WriteLine("The config is invalid, please use 'config set' command to reset one.");
                }
            }
            else
            {
                _console.WriteLine("Config is not existed, please use 'config set' command to set one.");
            }

            return null;
        }
    }
}