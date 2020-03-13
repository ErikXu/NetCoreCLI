using McMaster.Extensions.CommandLineUtils;

namespace NetCoreCLI.Commands.Config
{
    [Command("set", Description = "Set config")]
    public class SetCommand
    {
        private void OnExecute(IConfigService configService)
        {
            configService.Set();
        }
    }
}