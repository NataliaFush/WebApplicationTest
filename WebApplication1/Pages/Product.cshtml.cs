using Core.Entities;
using Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class ProductModel : PageModel
    {
        protected readonly IProductService _productService;

        public ProductModel(IProductService productService)
        {
            _productService = productService;
        }

        public Product? Product { get; set; }


        public async Task<IActionResult> OnGet(int id)
        {
            Product = await _productService.GetProductById(id);
            return Page();
        }
    }
}
