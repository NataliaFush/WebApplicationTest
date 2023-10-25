using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.PagesModel
{
    public class AddressModel
    {
        [HiddenInput]
        public int? Id { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "City", Prompt = "Enter your city")]
        public string? City { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Street", Prompt = "Enter your street")]
        public string? Street { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Name = "Indes", Prompt = "Enter your index")]
        public string? PostalCode { get; set; }
    }
}
