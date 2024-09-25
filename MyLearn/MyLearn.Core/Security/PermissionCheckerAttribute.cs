using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyLearn.Core.Service.PermissionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId;
        private IPermissionService PermissionService;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            PermissionService =(IPermissionService) context.HttpContext.RequestServices.GetService(typeof(IPermissionService));
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                string userName=context.HttpContext.User.Identity.Name.ToString();
                if (!PermissionService.CheckPermissionUser(userName,_permissionId))
                {
                    context.Result = new RedirectResult("/");

                }
            }
            else
            {
                 context.Result = new RedirectResult("/Login");
            }
        }
    }
}
