using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyLearn.DataLayer.Entities.Permission
{
    public class Permission
    {
        [Key]
        public int PermissionId { get; set; }
        [Display(Name = "عنوان نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(100, ErrorMessage = "لطفا {0} کمتر از {1} باشد")]
        public string PermissionTitle { get; set; }
        public int? ParentId { get; set; }

        #region Relations
        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }
        #endregion
    }
}
