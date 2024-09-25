using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.DataLayer.Entities.User
{
    public class UserRole
    {
        [Key]
        public int UR_ID { get; set; }
        public int RoleId { get; set; }
        public int UserId { get; set; }

        #region Relations
        public Role Role { get; set; }
        public User User { get; set; }
        #endregion
    }
}
