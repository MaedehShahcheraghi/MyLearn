using MyLearn.DataLayer.Entities.Course;
using MyLearn.DataLayer.Entities.Forum;
using MyLearn.DataLayer.Entities.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.User
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل را رعایت کنید")]
        public string Email { get; set; }
        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Password { get; set; }
        [Display(Name = "کد فعالسازی")]
        [MaxLength(50, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string ActiveCode { get; set; }
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        [Display(Name = "آواتار")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string AvatarName { get; set; }
        [Display(Name = "تاریخ ثبت نام")]
        public DateTime RegisterDate { get; set; }
        public bool IsDelete { get; set; } = false;

        #region Relations
        public List<UserRole> UserRoles { get; set; }

        public List<Course.Course> Courses { get; set; }
        public List<Order.Order> Orders { get; set; }
        public LinkedList<UserDiscount> userDiscounts { get; set; }
        public List<CourseComment> CourseComments { get; set; }
        public List<CourseVote> CourseVotes { get; set; }
        public List<Question> Questions { get; set; }
        public List<Answer> Answers { get; set; }

        #endregion

    }
}
