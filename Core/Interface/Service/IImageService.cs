using Core.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interface.Service
{
    public interface IImageService
    {
        Task<byte[]> GetImage(int? id, string? name);
    }
}
