using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Dtos.Admin.Users;
using MyLearn.Core.Service.UserService;

namespace MyLearn.Pages.Admin.Users
{
    public class EditUserModel : PageModel
    {
        private readonly IUserService userService;

        public EditUserModel(IUserService userService)
        {
            this.userService = userService;
        }
        [BindProperty]
        public EditUserViewModel editUserModel { get; set; }
        public void OnGet(int id)
        {
            editUserModel = userService.GetUserForEditAdmin(id);
            ViewData["Roles"]=userService.GetAllRoles();
            ViewData["UserRoles"]=userService.GetUserRoles(id);
        }

        public IActionResult OnPost(List<int> SelectedRoles) { 

            if(!ModelState.IsValid) { return Page(); }
            
            userService.EditUserForAdmin(editUserModel);
            userService.EditUsrRole(editUserModel.UserId, SelectedRoles);
            return RedirectToPage("Index");
        }
    }
}
