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
    public class WalletType
    {
        public WalletType()
        {
            
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }
        [Display(Name = "نوع عملیات")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(150, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string TypeTitle { get; set; }

        #region Reelations
        public List<Wallet> Wallets { get; set; }
        #endregion
    }
}
