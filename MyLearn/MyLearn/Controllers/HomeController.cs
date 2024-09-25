using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyLearn.Core.Service.Course;
using MyLearn.Core.Service.OrderService;
using MyLearn.Core.Service.UserService;
using MyLearn.Models;
using System.Diagnostics;

namespace MyLearn.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService userService;
        private readonly ICourseService courseService;
        private readonly IOrderService orderService;

        public HomeController(ILogger<HomeController> logger, IUserService userService, ICourseService courseService,IOrderService orderService)
        {
            _logger = logger;
            this.userService = userService;
            this.courseService = courseService;
            this.orderService = orderService;
        }
        [Route("ChargeWallet/{id}")]
        public IActionResult ChargeWallet(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
            HttpContext.Request.Query["Authority"] != ""
            )
            {
                var authority = HttpContext.Request.Query["Authority"];
                var wallet = userService.GetWalletById(id);
                var payment = new ZarinpalSandbox.Payment(wallet.Amount);
                var result = payment.Verification(authority).Result;
                if (result.Status == 100)
                {
                    wallet.IsPay = true;
                    userService.UpdateWallet(wallet);
                    ViewBag.refcode = result.RefId;
                    ViewBag.IsSuccess = true;
                }

            }
            return View();
        }
        [Route("FinallyOrderOnline/{id}")]
        public IActionResult FinallyOrderOnline(int id)
        {
            if (HttpContext.Request.Query["Status"] != "" && HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                      HttpContext.Request.Query["Authority"] != ""
                      )
            {
                var authority = HttpContext.Request.Query["Authority"];
               var order=orderService.GetOrderById(id);
                var payment = new ZarinpalSandbox.Payment(order.OrdrSum);
                var result = payment.Verification(authority).Result;
                if (result.Status == 100 && order != null)
                {
                    bool status = orderService.FinallyOrderOnline(id, User.Identity.Name);
                    if (status)
                    {
                        return Redirect($"/UserPanel/MyOrder/ShowOrder/{id}?finaly=true");

                    }


                }
            }
            return null;
        }
        public IActionResult GetSubGroups(int id)
        {
            List<SelectListItem> SubGroup = new List<SelectListItem>()
            {
                new SelectListItem()
                {
                    Value="0",
                    Text="لطفا انتخاب کنید"
                }
            };
            SubGroup.AddRange(courseService.GetSubGroupForManageCourse(id));
            return Json(SubGroup);
        }
        [HttpPost]
        [Route("file-upload")]
        public IActionResult UploadImage(IFormFile upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            if (upload.Length <= 0) return null;

            var fileName = Guid.NewGuid() + Path.GetExtension(upload.FileName).ToLower();



            var path = Path.Combine(
                Directory.GetCurrentDirectory(), "wwwroot/MyImages",
                fileName);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                upload.CopyTo(stream);

            }



            var url = $"{"/MyImages/"}{fileName}";


            return Json(new { uploaded = true, url });
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}