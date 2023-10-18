using Core;
using Core.Interface;
using Core.Interface.Service;
using Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace WebApplication1.Pages
{
    public class ImagesModel : PageModel
    {
        protected readonly IImageService _imageService;
        public ImagesModel(IImageService imageService)
        {
            _imageService= imageService;
        }
        public async Task<IActionResult> OnGet(int? id, string? name)
        {
            var byteArr = await _imageService.GetImage(id, name);
            return File(byteArr, $"image/{name?.Split('.').Last()}");
        }
      
    }
}
