using WebApplication1.Helpers;
using Core.Entities;
using Core.Enums;
using Core.Interface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebApplication1.PagesModel;

namespace WebApplication1.Pages
{
    public class UserUpdateModel : PageModel
    {
        protected readonly IUserService _userService;

        public UserUpdateModel(IUserService userService)
        {
            _userService = userService;
        }
        [BindProperty]
        public UserEditViewModel? User { get; set; }

        public IActionResult OnGet(Guid id)
        {
            var dbUser = _userService.GetUserById(id);
            if (dbUser != null)
            {
                User = new UserEditViewModel()
                {
                    Id = dbUser.Id,
                    Name = dbUser.Name,
                    Email = dbUser.Email,
                    Password = dbUser.Password,
                    Age = dbUser.Age,
                    Address = dbUser.Address != null ? new AddressModel()
                    {
                        Id = dbUser.Address.Id,
                        City = dbUser.Address.City,
                        PostalCode = dbUser.Address.PostalCode,
                        Street = dbUser.Address.Street
                    } : null,
                };
            }
            else
            {
                return BadRequest();
            }

            if (Request.IsAjaxRequest()) return Partial("_User", User);
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                if (Request.IsAjaxRequest()) return Partial("_User", User);
                return Page();
            }
            if (User != null)
            {
                var user = _userService.GetUserById(User.Id);
                if (user != null)
                {
                    user.Name = User.Name;
                    user.Age = User.Age;
                    user.Password = User.Password;

                    if (User.Address != null)
                    {
                        if (user.AddressId != 0 && user.Address != null)
                        {
                            user.Address.Street = User.Address.Street;
                            user.Address.City = User.Address.City;
                            user.Address.PostalCode = User.Address.PostalCode;
                        }
                        else
                        {
                            user.Address = new Address()
                            {
                                Street = User.Address.Street,
                                City = User.Address.City,
                                PostalCode = User.Address.PostalCode
                            };
                        }
                    }

                    if (_userService.UpdateUser(user))
                    {
                        User.Result = new ResultModel() { ResultType = ResultType.Success, Message = $"User {User.Name} was updated" };
                    }
                    else
                    {
                        User.Result = new ResultModel() { ResultType = ResultType.Error, Message = $"User {User.Name} was not updated" };
                    }
                }
            }
            else
            {
                User.Result = new ResultModel() { ResultType = ResultType.Error, Message = $"User {User.Name} was not updated" };
            }

            if (Request.IsAjaxRequest()) return Partial("_User", User);
            return Page();
        }
        public IActionResult OnPostCheckEmail(string email)
        {
            return new JsonResult(_userService.IsUsedEmail(email));
        }
    }
}

