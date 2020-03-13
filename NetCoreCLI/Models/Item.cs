using Newtonsoft.Json;

namespace NetCoreCLI.Models
{
    public class Item
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}