using McMaster.Extensions.CommandLineUtils;
using NetCoreCLI.Commands;

namespace NetCoreCLI
{
    [HelpOption(Inherited = true)]
    [Command(Description = "A tool to communicate with web api"),
     Subcommand(typeof(ConfigCommand))]
    class Program
    {
        public static void Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("Please submit a command.");
            app.ShowHelp();
            return 1;
        }
    }
}
