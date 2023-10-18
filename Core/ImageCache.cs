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
        private static Dictionary<string, byte[]> images;

        private static object lockObj = new object();
        static ImageCache()
        {
            images = new Dictionary<string, byte[]>();
        }

        public static byte[] GetImages(string key)
        {
            return images.GetValueOrDefault(key, new byte[0]);
        }

        public static void AddImage(string key, byte[] image)
        {
            if (!images.ContainsKey(key))
            {
                lock (lockObj)
                {
                    images.Add(key, image);
                }
            }
        }
        public static bool IsImageDownload(string key)
        {
            return images.ContainsKey(key);
        }
    }


}
