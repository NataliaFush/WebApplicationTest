using Core.Entities;
using Core.Interface;
using Core.Interface.Repository;
using Core.Interface.Service;

namespace Core.Service
{
    public class ImageService : IImageService
    {
        protected readonly IImageRepository _imageRepository;
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }


        public async Task<byte[]> GetImage(int? id, string? name)
        {
            if (!string.IsNullOrEmpty(name) && id.HasValue && id.Value != 0)
            {
                var imageName = id + "_image_" + name;
                if (ImageCache.IsImageDownload(imageName))
                {
                    return ImageCache.GetImages(imageName);
                }
                else
                {
                    if (IsImageInBase(name, out IImage dbImage))
                    {
                        if (dbImage.UpdateDate > DateTime.Now.Add(-TimeSpan.FromMinutes(30)))
                        {
                            ImageCache.AddImage(imageName, dbImage.Data);
                            return dbImage.Data;
                        }
                        else
                        {
                            var image = await GetImageFromSite(name, id);
                            if (image.Length > 0)
                            {
                                ImageCache.AddImage(imageName, image);
                                _imageRepository.UpdateImage(new Image()
                                {
                                    Data = image,
                                    Name = imageName,
                                    UpdateDate = DateTime.Now
                                });
                                return image;
                            }
                            else
                            {
                               return  dbImage.Data;
                            }
                        }
                    }
                    else
                    {
                        var image = await GetImageFromSite(name, id);
                        if (image.Length > 0)
                        {
                            ImageCache.AddImage(imageName, image);
                            _imageRepository.CreateImage(new Image()
                            {
                                Data = image,
                                Name = imageName,
                                UpdateDate = DateTime.Now
                            });
                            return image;
                        }
                    }
                }

            }

            return new byte[0];
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
