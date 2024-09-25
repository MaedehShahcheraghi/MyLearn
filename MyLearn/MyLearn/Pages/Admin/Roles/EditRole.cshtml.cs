using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyLearn.Core.Service.PermissionService;
using MyLearn.DataLayer.Entities.User;

namespace MyLearn.Pages.Admin.Roles
{
    public class EditRoleModel : PageModel
    {
        private readonly IPermissionService permissionService;

        public EditRoleModel(IPermissionService permissionService)
        {
            this.permissionService = permissionService;
        }
        [BindProperty]
        public Role Role { get; set; }

        public void OnGet(int id)
        {
            Role=permissionService.GetRoleById(id);
            ViewData["RolePermission"] = permissionService.GetRolePermissin(id);
            ViewData["Permissions"] = permissionService.GetAllPermission();

        }
        public IActionResult OnPost(List<int> selectedPermission)
        {
            permissionService.EditRole(Role);
            permissionService.EditRolePermission(Role.RoleId, selectedPermission);
            return RedirectToPage("Index");
        }
    }
}
