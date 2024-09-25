using MyLearn.Core.Dtos.OrderVMs;
using MyLearn.DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.OrderService
{
    public interface IOrderService
    {
        int AddOrder(string userName, int CourseId);
        int OrderSum(int orderId);
        Order GetOrderForUserPanel(int OrderId,string userName);    
        List<Order> GetAllOrders(string userName);
         bool FinallyOrder(int orderId);
        Order GetOrderById(int orderId);
        UseDiscountType UseDiscount(string discountCode, int orderId);
        bool FinallyOrderOnline(int orderId,string userName);
    }
}
