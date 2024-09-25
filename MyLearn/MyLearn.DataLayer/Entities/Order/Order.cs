using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Order
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Required]
        public int UserId { get; set; }
        public bool IsFinally { get; set; }
        [Required]
        public int OrdrSum { get; set; }
        public DateTime CreateDate { get; set; }

        #region Relations
        public User.User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion
    }
}
