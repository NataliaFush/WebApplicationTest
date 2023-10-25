using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.PagesModel
{
    public class UserEditViewModel
    {

        [HiddenInput]
        public Guid Id { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(50)]
        [Display(Name = "Name", Prompt = "Enter your name")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email", Prompt = "Enter your Email")]
        public string? Email { get; set; }

        public AddressModel? Address { get; set; }
       
        [Required]
        [Range(18, 99)]
        [Display(Name = "Age", Prompt = "Enter your Age")]
        public int? Age { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(16)]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Enter your Password")]
        public string? Password { get; set; }

        public ResultModel? Result { get; set; }
    }
}
