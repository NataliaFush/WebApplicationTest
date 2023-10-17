using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IProductService
    {
        Task<IEnumerable<Product?>> FindProductsAsync(string searchString);
        Task<IEnumerable<Product>> GetAllProductAsync();
        Task<Product> GetProductById(int id);

    }
}
