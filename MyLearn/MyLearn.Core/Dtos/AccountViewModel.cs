using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLearn.Core.Dtos
{
    
    public class RegisterViewModel
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
        [Display(Name = "تکرار  پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [Compare("Password", ErrorMessage = "پسورد و تکرار پسورد  با هم مطابقت ندارند")]
        public string RePassword { get; set; }
    }

    public class LoginViewModel
    {
      
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل را رعایت کنید")]
        public String Email { get; set; }
        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }

    }

    public class ForgotPaaswordViewModel
    {

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [EmailAddress(ErrorMessage = "لطفا فرمت ایمیل را رعایت کنید")]
        public String Email { get; set; }
    }
    public class ResetPssswordViewModel
    {
        public string ActiveCode { get; set; }
        [Display(Name = "پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Password { get; set; }
        [Display(Name = "تکرار  پسورد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        [Compare("Password", ErrorMessage = "پسورد و تکرار پسورد  با هم مطابقت ندارند")]
        public string RePassword { get; set; }
    }
}
