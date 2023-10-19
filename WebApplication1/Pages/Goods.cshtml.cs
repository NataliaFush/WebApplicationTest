using Core.Entities;
using Core.Healper;
using Core.Interface;
using Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace WebApplication1.Pages
{
    public class GoodsModel : PageModel
    {
               
        public IEnumerable<UserModel> Users { get; set; }
        public class Car
        {
            public string Name { get; set; }
            public int Price { get; set; }
            public Category Category { get; set; }
        }
        
        protected readonly IUserService _userService;

        public GoodsModel(IUserService userService)
        {
            _userService = userService;
        }

        public enum Category
        {
            None,
            Legkova,
            Vantajna,
            Bike
        }

        [BindProperty]
        public UserModel Input { get; set; }
        public Result Response { get; set; }
        public class Result
        {
            public ResultType ResultType { get; set; }
            public string? Message { get; set; }


        }
        public enum ResultType
        {
            Success,
            Error
        }


        public class UserModel
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

            public string? Address {get; set;}
            public int? AddressId { get; set; }

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

            [Required]
            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Password must be equals")]
            [Display(Name = "ConfirmPassword", Prompt = "Enter your ConfirmPassword")]
            public string? ConfirmPassword { get; set; }


        }
        //public void OnGet()
        //{
        //    //    Cars = new List<Car>()
        //    //    {
        //    //        new Car(){Category = Category.Legkova, Name = "Audi", Price = 8000},
        //    //        new Car(){Category = Category.Vantajna, Name = "Iveco", Price = 12000},
        //    //        new Car(){Category = Category.Vantajna, Name = "Reno", Price = 5600},
        //    //        new Car(){Category = Category.Vantajna, Name = "Volsvagen", Price = 152000},
        //    //        new Car(){Category = Category.Legkova, Name = "Honda", Price = 5600},
        //    //        new Car(){Category = Category.Bike, Name = "Aydi", Price = 80580}
        //    //    };
            
        //}

        public void OnPost()
        {

            ModelState.Remove("Input.Id");
            if (!string.IsNullOrEmpty(Input?.Email) && _userService.IsUsedEmail(Input?.Email))
            {
                ModelState.AddModelError("Input.Email", "This email can`t be used");
            }
            if (!ModelState.IsValid)
            {
                return;
            }
            if (Input != null)
            {
                var user = new User()
                {
                    Name = Input.Name,
                    Age = Input.Age,
                    Email = Input.Email,
                    Password = Input.Password
                };

                if (_userService.CreateUser(user))
                {
                    Response = new Result() { ResultType = ResultType.Success, Message = $"User {Input.Name} was created" };
                }
                else
                {
                    Response = new Result() { ResultType = ResultType.Error, Message = $"User {Input.Name} was not created" };
                }
            }
            else
            {
                Response = new Result() { ResultType = ResultType.Error, Message = $"User {Input.Name} was not created" };
            }

           
        }

        public IActionResult OnPostCheckEmail(string email)
        {
            return new JsonResult(_userService.IsUsedEmail(email));
        }
    }

}

