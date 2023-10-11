using Core.Intrerface;
using Core.Intrerface.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static WebApplication1.Pages.GoodsModel;

namespace WebApplication1.Pages
{
    public class UsersModel : PageModel
    {
        protected readonly IUserService _userService;
        public IEnumerable<IUser> Users { get; set; }

        public UsersModel(IUserService userService)
        {
            _userService = userService;
        }
        public void OnGet()
        {
            Users = _userService.GetAllUser();

        }
    }
}
