using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Dtos.UserPanel;
using MyLearn.Core.Service.UserService;

namespace MyLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService userService)
        {
            this.userService = userService;
        }
        [Authorize]
        public IActionResult Index()
        {
            return View(userService.GetDataForUserPanel(User.Identity.Name));
        }
        [Route("UserPanel/EditProfile")]
        public IActionResult EditProfile()
        {
            return View(userService.GetDataForEditProdileUserPanel(User.Identity.Name.ToString()));
        }
        [Route("UserPanel/EditProfile")]
        [HttpPost]
        public IActionResult EditProfile(EditProfileViewModel editProfile)
        {
            if (!ModelState.IsValid) { return View(editProfile); }
            userService.EditProfileForUserPanel(editProfile, User.Identity.Name.ToString());
            return Redirect("/Login?EditProfile=true");
        }
        [Route("UserPanel/ChangePassword")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [Route("UserPanel/ChangePassword")]
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordForUserPanel changePassword)
        {
            if (!ModelState.IsValid) { return View(); }
            userService.ChangePasswordViewModel(User.Identity.Name, changePassword);
            ViewBag.IsSuccess = true;
            return View();
        }

    }
}
