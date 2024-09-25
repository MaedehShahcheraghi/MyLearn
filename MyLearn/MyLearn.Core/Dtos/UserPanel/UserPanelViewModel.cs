using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Dtos.UserPanel
{
    public class UserPanelViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public int WalletSum { get; set; }
        public DateTime RegisterDate { get; set; }
    }
    public class EditProfileViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AvateName { get; set; }
        public IFormFile? Avatar { get; set; }
    }

    public class ChangePasswordForUserPanel
    {
        public string Password { get; set; }
        public string NewPassword { get; set; }
        [Compare("NewPassword")]
        public string RePassword { get; set; }
    }

}
