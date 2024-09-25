using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.PermissionService;
using MyLearn.DataLayer.Entities.User;

namespace MyLearn.Pages.Admin.Roles
{
    public class CreateRoleModel : PageModel
    {
        private readonly IPermissionService permissionService;

        public CreateRoleModel(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }
        [BindProperty]
        public Role role { get; set; }
        public void OnGet()
        {
            ViewData["Permission"] = permissionService.GetAllPermission();
        }
        public IActionResult OnPost(List<int> selectedPermission)
        {
           
            int roleId=permissionService.AddRole(role);
            permissionService.AddPermissionRole(roleId, selectedPermission);
            return RedirectToPage("Index");
        }
    }
}
