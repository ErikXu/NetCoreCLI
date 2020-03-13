using McMaster.Extensions.CommandLineUtils;
using NetCoreCLI.Models;

namespace NetCoreCLI.Commands.Item
{
    [Command("create", Description = "Create an item")]

    public class CreateCommand
    {
        private void OnExecute(IConsole console, IItemClient itemClient)
        {
            var form = new ItemForm
            {
                Name = Prompt.GetString("Input the name:"),
                Age = Prompt.GetInt("Input the age:")
            };
            var response = itemClient.Create(form).Result;
            console.WriteLine(response);
        }
    }
}