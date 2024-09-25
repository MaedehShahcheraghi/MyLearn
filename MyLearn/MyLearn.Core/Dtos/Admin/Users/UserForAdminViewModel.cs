using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLearn.Core.Dtos.Admin.Users
{
    public class UserForAdminViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public DateTime RegisterDate { get; set; }



    }

    public class CreatUserViewModel
    {

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل را رعایت کنید")]
        public String Email { get; set; }
        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Password { get; set; }

        public IFormFile UserAvatar { get; set; }
    }

    public class EditUserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string UserName { get; set; }
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل را رعایت کنید")]
        public String Email { get; set; }
        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Password { get; set; }

        public string AvatarName { get; set; }
        public IFormFile? UserAvatar { get; set; }
    }
}
