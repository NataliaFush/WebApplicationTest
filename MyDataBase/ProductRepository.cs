using Core.Entities;
using Core.Enums;
using Core.Interface;
using Core.Interface.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyDataBase.Models
{
    public class ProductRepository : IProductRepository
    {
        private const string productsKey = "productsKeyex";
        private readonly IMemoryCache _memoryCache;
        public ProductRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            if (!_memoryCache.TryGetValue(productsKey, out ProductsDb? data))
            {
                using var client = new HttpClient();
                using HttpResponseMessage response = await client.GetAsync("https://dummyjson.com/products?limit=100");

                var str = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<ProductsDb>(str, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                data = result;
                var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1));
                _memoryCache.Set(productsKey, data,  cacheEntryOptions);
            }
            return data.Products;
        }

        //public async Task<IEnumerable<Images>> GetAllImagesAsync()
        //{

        //}




    }
}
