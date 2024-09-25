using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Dtos;
using MyLearn.Core.Generators;
using MyLearn.Core.Security;
using MyLearn.Core.Senders;
using MyLearn.Core.Service.UserService;
using System.Security.Claims;
using TopLearn.Core.Convertors;

namespace MyLearn.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;
        private readonly IViewRenderService renderService;

        public AccountController(IUserService userService,IViewRenderService renderService)
        {
            this.userService = userService;
            this.renderService = renderService;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region Register
        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }
        [Route("Register")]
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) { return View(registerViewModel); }
            if (userService.IsExistEamil(registerViewModel.Email)) { ModelState.AddModelError(registerViewModel.Email, "ایمیل وارد شده استفاده شده است."); return View(registerViewModel); }
            if (userService.IsExistUserName(registerViewModel.UserName)) { ModelState.AddModelError(registerViewModel.UserName, "نام کاربری وارد شده استفاده شده است."); return View(registerViewModel); }
            var user = userService.RegisterUser(registerViewModel);
            var body = renderService.RenderToStringAsync("_ActiveEmail", user);
            SendEmail.Send(user.Email, "فعالسازی حساب کاربری", body);
            return View("SuccessRegister", user);
        }


        public IActionResult ActiveAccount(string id)
        {
            var user = userService.GerUserByActiveCode(id);
            if (user == null) ViewBag.IsActive = false;
            ViewBag.IsActive = userService.ActiveAccount(user);
            return View();
        }
        #endregion
        #region Login
        [Route("Login")]
        public IActionResult Login(bool EditProfile=false)
        {
            ViewBag.EditProfile= EditProfile;
            return View();
        }
        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) { return View(loginViewModel); }
            var user = userService.GetUserForLogin(loginViewModel.Email, loginViewModel.Password);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربر مورد نظر یافت نشد");
                return View(loginViewModel);
            }
            if (user.IsActive)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.UserId.ToString()),
                new Claim(ClaimTypes.Name,user.UserName.ToString())
            };
                var claimidentiy = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimpricipal = new ClaimsPrincipal(claimidentiy);
                var propertis = new AuthenticationProperties()
                {
                    IsPersistent = loginViewModel.RememberMe
                };
                await HttpContext.SignInAsync(claimpricipal, propertis);
                ViewBag.IsSuccess = true;
            }
            else
            {
                ModelState.AddModelError("Email", "کابر گرامی  حساب کاربری شما فعال نشده است.");
                return View(loginViewModel);
            }
            ViewBag.EditProfile = false;
            return View(loginViewModel);
        }
        #endregion
        #region LogOut
        [Route("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        #endregion
        #region ForgotPassword
        [Route("ForgotPassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [Route("ForgotPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPaaswordViewModel forgotPaasword)
        {
            if (!ModelState.IsValid)
            {
                return View(forgotPaasword);
            }
            var user = userService.GetUserByEamil(forgotPaasword.Email);
            if (user == null)
            {
                ModelState.AddModelError("Email", "کاربر مورد نظر یافت نشد");
                return View(forgotPaasword);
            }
            string bodyEmail = renderService.RenderToStringAsync("_ForgotPassword", user);
            await SendEmail.Send(user.Email, "بازیابی کلمه عبور", bodyEmail);
            ViewBag.IsSuccess =true;
            return View(forgotPaasword);
        }

        #endregion
        #region ResetPassword
        public IActionResult ResetPassword(string id)
        {
            
            return View(new ResetPssswordViewModel()
            {
                ActiveCode= id
            });
        }
        [HttpPost]
        public IActionResult ResetPassword(ResetPssswordViewModel resetPsssword)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user=userService.GerUserByActiveCode(resetPsssword.ActiveCode);
            if (user == null) return NotFound();
            user.Password = PasswordHelper.EncodePasswordMd5(resetPsssword.Password);
            user.ActiveCode = NameGenerator.GenerateUnicCode();
            userService.UpdateUser(user);
            return Redirect("/Login");
        }

        #endregion
    }
}
