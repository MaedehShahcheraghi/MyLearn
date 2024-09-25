using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Dtos.UserPanel;
using MyLearn.Core.Service.UserService;


namespace MyLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        private readonly IUserService userService;

        public WalletController(IUserService userService)
        {
            this.userService = userService;
        }
        public IActionResult Index()
        {
            ViewBag.ListWalletUser = userService.GetWallets(User.Identity.Name);
            return View();
        }
        [HttpPost]
        public IActionResult Index(ChargeWalletViewModel chargeWallet)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.ListWalletUser = userService.GetWallets(User.Identity.Name);
                return View(chargeWallet);
            }
            int walletId = userService.AddWallet(User.Identity.Name, chargeWallet.Amount, "شارژ حساب",1);
            var payment = new ZarinpalSandbox.Payment(chargeWallet.Amount);
            var paymentrequest = payment.PaymentRequest("شارژ حساب", "http://localhost:5213/ChargeWallet/" + walletId, "maedeh.shahcheraghi1384@gmail.com", "09925772866");
            if (paymentrequest.Result.Status == 100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + paymentrequest.Result.Authority);

            }

            return RedirectToAction("Index");
        }
    }
}
