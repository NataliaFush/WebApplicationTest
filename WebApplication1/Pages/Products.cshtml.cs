using Core.Interface.Service;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Core.Interface.Repository;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Core.Healper;
using System.Diagnostics;
using Core.Enums;

namespace WebApplication1.Pages
{
    public class ProductsModel : PageModel
    {
     

        protected readonly IProductService _productService;

        public ProductsModel(IProductService productService)
        {
            _productService = productService;
        }

        public IEnumerable<Product?> Products { get; set; }


        public async Task<IActionResult> OnGet(string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || searchString.Length < 3)
            {
                Products = await _productService.GetAllProductAsync();
            }
            else
            {
                Products = await _productService.FindProductsAsync(searchString);
            }
            //var st = new TestSpeed();
            //Stopwatch watch = new Stopwatch();
            
            var a = "http://www.example.com".IsStringLink();

            var en = DayType.Friday.GetDisplayName(); 

            return Page();

        }

    }
}
