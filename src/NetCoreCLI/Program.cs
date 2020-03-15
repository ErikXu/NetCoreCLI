using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using NetCoreCLI.Commands.Config;
using NetCoreCLI.Commands.Item;

namespace NetCoreCLI
{
    [HelpOption(Inherited = true)]
    [Command(Description = "A tool to communicate with web api"),
     Subcommand(typeof(ConfigCommand), typeof(ItemCommand))]
    class Program
    {
        public static int Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(PhysicalConsole.Singleton);
            serviceCollection.AddSingleton<IConfigService, ConfigService>();
            serviceCollection.AddHttpClient<IItemClient, ItemClient>();

            var services = serviceCollection.BuildServiceProvider();

            var app = new CommandLineApplication<Program>();
            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(services);

            var console = (IConsole)services.GetService(typeof(IConsole));

            try
            {
                return app.Execute(args);
            }
            catch (UnrecognizedCommandParsingException ex)
            {
                console.WriteLine(ex.Message);
                return -1;
            }
        }

        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("Please specify a command.");
            app.ShowHelp();
            return 1;
        }
    }
}
