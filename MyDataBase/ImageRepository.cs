using Core.Interface;
using Core.Interface.Repository;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.Extensions.Caching.Memory;
using MyDataBase.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDataBase
{
    public class ImageRepository : IImageRepository
    {
        protected readonly MyDbContext _dbcontext;

        public ImageRepository(MyDbContext myDb)
        {
            _dbcontext = myDb;
        }

        public IImage? GetImageByName(string name)
        {
            return _dbcontext.Image.FirstOrDefault(x => x.Name == name);
        }

        public bool CreateImage(IImage image)
        {
            try
            {
                _dbcontext.Image.Add(new Image()
                {
                    Data = image.Data,
                    Name = image.Name,
                    UpdateDate = DateTime.Now
                });
                _dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public bool UpdateImage(IImage image)
        {
            try
            {
                var dbImage = _dbcontext.Image.FirstOrDefault(x => x.Name == image.Name);
                if (dbImage != null)
                {
                    dbImage.UpdateDate = DateTime.Now;
                    dbImage.Name = image.Name;
                    _dbcontext.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }





    }
}
