using Core.Entities;
using Core.Interface.Repository;
using Core.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public class ProductService : IProductService
    {
        protected readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> GetAllProductAsync()
        {
            return await _productRepository.GetAllProductAsync();
        }

        public async Task<IEnumerable<Product>> FindProductsAsync(string searchString)

        {
            return (await GetAllProductAsync())
                .Where(x => x.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase) || x.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase));
        }

        public async Task<Product> GetProductById(int id)
        {
            var products = await GetAllProductAsync();
            return products.FirstOrDefault(x => x.Id == id);
        }


    }
}
