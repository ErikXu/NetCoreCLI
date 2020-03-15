using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Item
{
    [Command("item", Description = "Manage item"),
     Subcommand(typeof(CreateCommand), typeof(GetCommand), typeof(ListCommand), typeof(DeleteCommand))]
    public class ItemCommand
    {
        private int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.Error.WriteLine("Please submit a sub command.");
            app.ShowHelp();
            return 1;
        }
    }
}