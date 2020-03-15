using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Item
{
    [Command("get", Description = "Get an item")]
    public class GetCommand
    {
        [Argument(0, Name = "id")]
        [Required(ErrorMessage = "The id is required.")]
        public string Id { get; set; }

        private void OnExecute(IConsole console, IItemClient itemClient)
        {
            var response = itemClient.Get(Id).Result;
            console.WriteLine(response);
        }
    }
}