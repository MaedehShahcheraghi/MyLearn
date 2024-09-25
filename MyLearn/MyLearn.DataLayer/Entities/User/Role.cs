using MyLearn.DataLayer.Entities.Permission;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.User
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Display(Name = "نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }=false;

        #region Relations
        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        #endregion
    }
}
