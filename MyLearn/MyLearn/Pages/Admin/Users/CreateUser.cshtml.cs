using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Dtos.Admin.Users;
using MyLearn.Core.Generators;
using MyLearn.Core.Security;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.User;

namespace MyLearn.Pages.Admin.Users
{
    [PermissionChecker(3)]
    public class CreateUserModel : PageModel
    {
        private readonly IUserService userService;

        public CreateUserModel(IUserService userService)
        {
            this.userService = userService;
        }
        [BindProperty]
        public CreatUserViewModel CreatUserViewModel { get; set; }

        public void OnGet()
        {
            ViewData["Roles"]=userService.GetAllRoles();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
           if (!ModelState.IsValid) { return Page(); }
           int userId= userService.AddUserForAdmin(CreatUserViewModel);
            userService.AddUserRole(userId, SelectedRoles);
            return RedirectToPage("Index");

        }
    }
}
