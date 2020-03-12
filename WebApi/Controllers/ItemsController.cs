using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMemoryCache _cache;
        private readonly string _key = "items";

        public ItemsController(IMemoryCache memoryCache)
        {
            _cache = memoryCache;
        }

        [HttpGet]
        public IActionResult List()
        {
            var items = _cache.Get<List<Item>>(_key);
            return Ok(items);
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var item = _cache.Get<List<Item>>(_key).FirstOrDefault(n => n.Id == id);
            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(ItemForm form)
        {
            var items = _cache.Get<List<Item>>(_key) ?? new List<Item>();

            var item = new Item
            {
                Id = Guid.NewGuid().ToString("N"),
                Name = form.Name,
                Age = form.Age
            };

            items.Add(item);

            _cache.Set(_key, items);
            
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var items = _cache.Get<List<Item>>(_key);

            var item = items.SingleOrDefault(n => n.Id == id);
            if (item != null)
            {
                items.Remove(item);
            }

            _cache.Set(_key, items);

            return Ok();
        }
    }
}