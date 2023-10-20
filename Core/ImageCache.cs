using Core.Interface;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class ImageCache
    {
        private static Dictionary<string, CasheElement<byte[]>> images;

        private static object lockObj = new object();
        static ImageCache()
        {
            images = new Dictionary<string, CasheElement<byte[]>> ();
        }

        public static byte[] GetImages(string key)
        {
            return GetPrivateImage(key) ?? new byte[0];
        }

        public static void AddImage(string key, byte[] image, DateTime expiredDate)
        {
            if (!images.ContainsKey(key))
            {
                lock (lockObj)
                {
                    images.Add(key, new CasheElement< byte[] > { Value = image, EndDate = expiredDate });
                }
            }
        }
        public static bool IsImageDownload(string key)
        {
            return GetPrivateImage(key) != null;
        }

        public static bool IsImageDownload(string key, out byte[]? image)
        {
            image = GetPrivateImage(key);
            return image != null;
        }

        private static byte[]? GetPrivateImage(string key)
        {
            var element = images.GetValueOrDefault(key, null);

            if (element == null) return null;
            else
            {
                if (element.EndDate < DateTime.Now)
                {
                    lock (lockObj)
                    {
                        images.Remove(key);
                    }
                    return null;
                }
            }
            return element.Value;
        }
    }

    public class CasheElement<T>
    {
        public T Value { get; set; }
        public DateTime EndDate { get; set; }
       
    }


}
