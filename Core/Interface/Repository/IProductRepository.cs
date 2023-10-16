using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllProductAsync();
    }
}
