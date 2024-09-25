using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.User;

namespace MyLearn.Pages.Admin.Roles
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }
        public List<Role> Roles { get; set; }
        public void OnGet()
        {
            Roles=userService.GetAllRoles();
        }
    }
}
