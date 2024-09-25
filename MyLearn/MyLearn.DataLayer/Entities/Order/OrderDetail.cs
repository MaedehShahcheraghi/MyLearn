using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.Order
{
    public class OrderDetail
    {
        [Key]
        public int DetailId { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int CourseId { get; set; }
        public int Count { get; set; }
        public int Price { get; set; }


        #region Relations
        public Course.Course Course { get; set; }
        public Order Order { get; set; }

        #endregion
    }
}
