using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Item
{
    [Command("list", Description = "Get item list")]
    public class ListCommand
    {
        private void OnExecute(IConsole console, IItemClient itemClient)
        {
            var response = itemClient.List().Result;
            console.WriteLine(response);
        }
    }
}