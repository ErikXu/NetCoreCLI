using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NetCoreCLI.Models;
using Newtonsoft.Json;

namespace NetCoreCLI
{
    public interface IItemClient
    {
        Task<string> Create(ItemForm form);

        Task<string> Get(string id);

        Task<string> List();

        Task<string> Delete(string id);
    }

    public class ItemClient : IItemClient
    {
        public HttpClient Client { get; }

        public ItemClient(HttpClient client, IConfigService configService)
        {
            var config = configService.Get();
            if (config == null)
            {
                return;
            }

            client.BaseAddress = new Uri(config.Endpoint);

            Client = client;
        }

        public async Task<string> Create(ItemForm form)
        {
            var content = new StringContent(JsonConvert.SerializeObject(form), Encoding.UTF8, "application/json");
            var result = await Client.PostAsync("/api/items", content);

            if (result.IsSuccessStatusCode)
            {
                var stream = await result.Content.ReadAsStreamAsync();
                var item = Deserialize<Item>(stream);
                return $"Item created, info:{item}";
            }

            return "Error occur, please again later.";
        }

        public async Task<string> Get(string id)
        {
            var result = await Client.GetAsync($"/api/items/{id}");

            if (result.IsSuccessStatusCode)
            {
                var stream = await result.Content.ReadAsStreamAsync();
                var item = Deserialize<Item>(stream);

                var response = new StringBuilder();
                response.AppendLine($"{"Id".PadRight(40, ' ')}{"Name".PadRight(20, ' ')}Age");
                response.AppendLine($"{item.Id.PadRight(40, ' ')}{item.Name.PadRight(20, ' ')}{item.Age}");
                return response.ToString();
            }

            return "Error occur, please again later.";
        }

        public async Task<string> List()
        {
            var result = await Client.GetAsync($"/api/items");

            if (result.IsSuccessStatusCode)
            {
                var stream = await result.Content.ReadAsStreamAsync();
                var items = Deserialize<List<Item>>(stream);

                var response = new StringBuilder();
                response.AppendLine($"{"Id".PadRight(40, ' ')}{"Name".PadRight(20, ' ')}Age");

                if (items != null && items.Count > 0)
                {
                    foreach (var item in items)
                    {
                        response.AppendLine($"{item.Id.PadRight(40, ' ')}{item.Name.PadRight(20, ' ')}{item.Age}");
                    }
                }
                
                return response.ToString();
            }

            return "Error occur, please again later.";
        }

        public async Task<string> Delete(string id)
        {
            var result = await Client.DeleteAsync($"/api/items/{id}");

            if (result.IsSuccessStatusCode)
            {
                return $"Item {id} deleted.";
            }

            if (result.StatusCode == HttpStatusCode.NotFound)
            {
                return $"Item {id} not found.";
            }

            return "Error occur, please again later.";
        }

        private static T Deserialize<T>(Stream stream)
        {
            using var reader = new JsonTextReader(new StreamReader(stream));
            var serializer = new JsonSerializer();
            return (T)serializer.Deserialize(reader, typeof(T));
        }
    }
}