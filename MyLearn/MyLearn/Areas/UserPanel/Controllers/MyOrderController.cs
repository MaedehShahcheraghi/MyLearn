using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLearn.Core.Dtos.OrderVMs;
using MyLearn.Core.Service.OrderService;
using ZarinpalSandbox;

namespace MyLearn.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class MyOrderController : Controller
    {
        private readonly IOrderService orderService;

        public MyOrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        public IActionResult Index()
        {
            var orders = orderService.GetAllOrders(User.Identity.Name);
            return View(orders);
        }
      
        public IActionResult ShowOrder(int id,bool fainally=false,string? type="")
        {
            var order = orderService.GetOrderForUserPanel(id, User.Identity.Name);
            ViewBag.fainally = fainally;  
            ViewBag.type = type;    
            return View(order);
        }

        public IActionResult FinallyOrder(int id)
        {
            var Isfinally= orderService.FinallyOrder(id);
            
            return RedirectToAction("ShowOrder",new {id=id, fainally=Isfinally});
        }

        public IActionResult UseDiscount(int orderId,string code)
        {
            UseDiscountType userdiscount= orderService.UseDiscount(code, orderId);
            return RedirectToAction("ShowOrder", new { id =orderId, type=userdiscount.ToString() });

        }
        public IActionResult OnlineOrder(int id)
        {
            var order=orderService.GetOrderById(id);
            if (order == null)
                return NotFound();
            var paymment = new Payment(order.OrdrSum);
            var result = paymment.PaymentRequest($"پرداخت انلاین فاکتور شماره ی {order.OrderId}", "http://localhost:5213/FinallyOrderOnline/"+order.OrderId, "maedeh.shahcheraghi1384@gmail.com", "09925772866");
            if (result.Result.Status==100)
            {
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + result.Result.Authority);
            }
            return Redirect("Index");
        }
    }
}
