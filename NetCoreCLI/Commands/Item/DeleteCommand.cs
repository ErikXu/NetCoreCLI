using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Item
{
    [Command("delete", Description = "Delete an item")]
    public class DeleteCommand
    {
        [Argument(0, Name = "id")]
        [Required(ErrorMessage = "The id is required.")]
        public string Id { get; set; }

        private void OnExecute(IConsole console, IItemClient itemClient)
        {
            var response = itemClient.Delete(Id).Result;
            console.WriteLine(response);
        }
    }
}