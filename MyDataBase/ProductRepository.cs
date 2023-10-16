using Core.Entities;
using Core.Enums;
using Core.Interface;
using Core.Interface.Repository;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            using var client = new HttpClient();
            using HttpResponseMessage response = await client.GetAsync("https://dummyjson.com/products");

            var str = await response.Content.ReadAsStringAsync();

            var result = JsonSerializer.Deserialize<ProductsDb>(str, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });

            return result?.Products;
        }
    }
}
