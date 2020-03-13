using Newtonsoft.Json;

namespace NetCoreCLI.Models
{
    public class Config
    {
        public string Endpoint { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}