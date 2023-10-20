using Core.Entities;
using Core.Interface;
using Core.Interface.Repository;
using Core.Interface.Service;
using Microsoft.Extensions.Options;
using static Azure.Core.HttpHeader;

namespace Core.Service
{
    public class ImageService : IImageService
    {
        protected readonly IImageRepository _imageRepository;
        protected readonly AppConf _config;
        public ImageService(IImageRepository imageRepository, IOptions<AppConf> options)
        {
            _imageRepository = imageRepository;
            _config = options.Value;
        }


        public async Task<byte[]> GetImage(int? id, string? name)
        {
            if (!string.IsNullOrEmpty(name) && id.HasValue && id.Value != 0)
            {
                var imageName = GetImageName(id.Value, name);
                if (ImageCache.IsImageDownload(imageName, out byte[]? image))
                {
                    return image ?? new byte[0];
                }
                else
                {
                    if (IsImageInBase(imageName, out IImage dbImage))
                    {
                        if (dbImage.UpdateDate.Add(_config.ElapsedDbTime) > DateTime.Now)
                        {
                            ImageCache.AddImage(imageName, dbImage.Data, DateTime.Now.Add(_config.ElapsedCacheTime));
                            return dbImage.Data;
                        }
                        else
                        {
                            return await UpdateImage(_imageRepository.UpdateImage, name, id.Value, dbImage.Data);
                        }
                    }
                    else
                    {
                        return await UpdateImage(_imageRepository.CreateImage, name, id.Value, new byte[0]);
                    }
                }

            }

            return new byte[0];
        }

        private async Task<byte[]> UpdateImage(Func<IImage, bool> func, string name, int id, byte[] defaultData)
        {
            var image = await GetImageFromSite(name, id);
            if (image.Length == 0) { return defaultData; }

            var imageName = GetImageName(id, name);
            var iImage = new Image()
            {
                Data = image,
                Name = imageName,
                UpdateDate = DateTime.Now
            };

            ImageCache.AddImage(imageName, image, DateTime.Now.Add(_config.ElapsedCacheTime));
            func?.Invoke(iImage);
            return image;

        }

        private string GetImageName(int id, string name)
        {
            return id + "_image_" + name;
        }

        private bool IsImageInBase(string imageName, out IImage image)
        {
            image = _imageRepository.GetImageByName(imageName)!;
            return image != null;
        }



        private async Task<byte[]> GetImageFromSite(string? name, int? id)
        {
            using HttpClient client = new HttpClient();
            byte[] byteArr = new byte[0];

            try
            {
                using var responce = await client.GetAsync($"https://i.dummyjson.com/data/products/{id}/{name}");

                if (responce != null && responce.IsSuccessStatusCode)
                {
                    byteArr = await responce.Content.ReadAsByteArrayAsync();
                }
            }
            catch (Exception ex)
            { }

            return byteArr;
        }
    }
}
