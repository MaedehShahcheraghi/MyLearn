using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Entities.User;

namespace MyLearn.Pages.Admin.Users
{
    public class DeleteUserModel : PageModel
    {
        private readonly IUserService userService;

        public DeleteUserModel(IUserService userService)
        {
            this.userService = userService;
        }
        public User user { get; set; }
        public void OnGet(int id)
        {
            ViewData["userId"] = id.ToString();
            user =userService.GetUserByUserId(id);
        }
        public IActionResult OnPost(int id)
        {
            userService.DeleteUserForAdmin(id);
            return RedirectToPage("Index");
        }
    }
}
