using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLearn.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [ForeignKey("WalletType")]
        public int TypeId { get; set; }
        [Display(Name = "کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int UserId { get; set; }
        [Display(Name = "مبلغ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Amount { get; set; }
        [Display(Name = "توضیحات")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string Descreption { get; set; }
        [Display(Name = "تایید شده")]
        public bool IsPay { get; set; }
        [Display(Name = "تاریخ وساعت")]
        public DateTime CreateDate { get; set; }

        #region Realtions

        public List<User.User> Users { get; set; }
        public WalletType WalletType { get; set; }
        #endregion
    }
}
