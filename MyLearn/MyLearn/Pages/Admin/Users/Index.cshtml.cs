using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Dtos.Admin.Users;
using MyLearn.Core.Service.UserService;

namespace MyLearn.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        private readonly IUserService userService;

        public IndexModel(IUserService userService)
        {
            this.userService = userService;
        }
        public Tuple<List<UserForAdminViewModel>, int, int> Users { get; set; }
        public void OnGet(string userName="",string eamil="",int pageId=1)
        {
            Users=userService.GetUsersForAdmin(userName,eamil,pageId);
          
        }
    }
}
