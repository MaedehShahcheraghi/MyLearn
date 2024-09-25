using Microsoft.EntityFrameworkCore;
using MyLearn.Core.Dtos.OrderVMs;
using MyLearn.Core.Service.UserService;
using MyLearn.DataLayer.Context;
using MyLearn.DataLayer.Entities.Order;
using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly MyLearnContext myLearnContext;
        private readonly IUserService userService;

        public OrderService(MyLearnContext myLearnContext,IUserService userService)
        {
            this.myLearnContext = myLearnContext;
            this.userService = userService;
        }
        public int AddOrder(string userName, int CourseId)
        {
            var userid = myLearnContext.Users.FirstOrDefault(x => x.UserName == userName).UserId;
            var course = myLearnContext.Courses.Find(CourseId);
            var order = myLearnContext.Orders.Where(o => o.UserId == userid && !o.IsFinally).FirstOrDefault();
            if (order == null)
            {
                order = new DataLayer.Entities.Order.Order()
                {
                    UserId = userid,
                    IsFinally = false,
                    CreateDate = DateTime.Now,
                    OrderDetails = new List<DataLayer.Entities.Order.OrderDetail>()
                    {
                        new DataLayer.Entities.Order.OrderDetail()
                        {
                            CourseId = CourseId,
                            Price=course.CoursePrice,
                            Count=1

                        }
                    },
                    OrdrSum = course.CoursePrice
                };
                myLearnContext.Orders.Add(order);
                myLearnContext.SaveChanges();

            }
            else
            {
                var orderDetail = myLearnContext.OrderDetails.Where(od => od.OrderId == order.OrderId && od.CourseId == CourseId).FirstOrDefault();
                if (orderDetail == null)
                {
                    orderDetail = new DataLayer.Entities.Order.OrderDetail()
                    {
                        OrderId = order.OrderId,
                        CourseId = CourseId,
                        Price = course.CoursePrice,
                        Count = 1
                    };
                    myLearnContext.OrderDetails.Add(orderDetail);   
                    myLearnContext.SaveChanges();   
                }
                else
                {
                    orderDetail.Count += 1;
                    myLearnContext.OrderDetails.Update(orderDetail); 

                }
                order.OrdrSum = OrderSum(order.OrderId);
                myLearnContext.SaveChanges();
            }
            return order.OrderId;


        }

        public bool FinallyOrder(int orderId)
        {
           
            var order=GetOrderById(orderId);
          
           var user=userService.GetUserByUserId(order.UserId);
            if (userService.GetBallanceUserWallet(user.UserName)>order.OrdrSum)
            {
                order.IsFinally = true;
                List<int> courses = order.OrderDetails.Select(od => od.CourseId).ToList();
                foreach (var item in courses)
                {
                    myLearnContext.UserCourses.Add(new UserCourse()
                    {
                        UserId = order.UserId,
                        CourseId = item
                    });
                }
                myLearnContext.SaveChanges();
                userService.AddWallet(user.UserName, order.OrdrSum, "برداشت از کیف پول", 2);
                return true;
            }

            else { return false; }
        }

        public bool FinallyOrderOnline(int orderId, string userName)
        {
            int userId=userService.GetUserIdByUserName(userName);
            var order=myLearnContext.Orders.Include(o=> o.OrderDetails).FirstOrDefault(o=> o.OrderId ==orderId && o.UserId==userId);
            List<int> CoursesId=order.OrderDetails.Select(o=> o.CourseId).ToList();
            order.IsFinally = true; 
            foreach (var item in CoursesId)
            {
                var usercourse = new UserCourse()
                {
                    UserId = order.UserId,
                    CourseId = item
                };
                myLearnContext.UserCourses.Add(usercourse);
                myLearnContext.SaveChanges();
            }
            myLearnContext.SaveChanges();

            return true;
        }

        public List<Order> GetAllOrders(string userName)
        {
            int userid = myLearnContext.Users.FirstOrDefault(x => x.UserName == userName).UserId;
            return myLearnContext.Orders.Where(o=> o.UserId == userid).ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return myLearnContext.Orders.Include(od=> od.OrderDetails).FirstOrDefault(o=> o.OrderId==orderId);
        }

        public Order GetOrderForUserPanel(int OrderId, string userName)
        {
            int userid= myLearnContext.Users.FirstOrDefault(x => x.UserName == userName).UserId;
            return myLearnContext.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Course).
                FirstOrDefault(o => o.OrderId == OrderId && o.UserId == userid);
        }

        public int OrderSum(int orderId)
        {
            var prices = myLearnContext.OrderDetails.Where(od => od.OrderId == orderId).Select(od => od.Price*od.Count).Sum();

            return prices;
        }

        public UseDiscountType UseDiscount(string discountCode, int orderId)
        {
            var order = GetOrderById(orderId);
            var discount = myLearnContext.Discounts.FirstOrDefault(c => c.DiscountCode == discountCode);
            if (discount == null)
            {
                return UseDiscountType.NotFound;
            }
            if (discount.UsableCount <= 0)
            {
                return UseDiscountType.Finshed;
            }
            if (myLearnContext.UserDiscounts.Any(u => u.UserId == order.UserId && u.DiscountId == discount.DiscountId))
            {
                return UseDiscountType.UserUsed;
            }
            if (discount.EndDate!=null && discount.EndDate <= DateTime.Now)
            {
                return UseDiscountType.ExpaireDate;
            }
            if (discount.StartDate!=null && discount.StartDate >= DateTime.Now)
            {
                return UseDiscountType.NoYet;
            }

            order.OrdrSum = (order.OrdrSum * discount.DiscountPercent) / 100;
            discount.UsableCount -= 1;
            var usserdiscount = new UserDiscount()
            {
                DiscountId = discount.DiscountId,
                UserId = order.UserId
            };

            myLearnContext.UserDiscounts.Add(usserdiscount);
            myLearnContext.SaveChanges();

            return UseDiscountType.Success;

        }
    }
}
