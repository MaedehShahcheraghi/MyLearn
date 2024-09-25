using MyLearn.DataLayer.Context;
using MyLearn.DataLayer.Entities.Permission;
using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.PermissionService
{
    public class PermissionService : IPermissionService
    {
        private readonly MyLearnContext context;

        public PermissionService(MyLearnContext context)
        {
            this.context = context;
        }

        public void AddPermissionRole(int roleId, List<int> SelectedPermission)
        {
            foreach (var permission in SelectedPermission)
            {
                context.RolePermissions.Add(new RolePermission() { RoleId = roleId, PermissionId = permission });
            }
            context.SaveChanges();
        }

        public int AddRole(Role role)
        {
            context.Roles.Add(role);
            context.SaveChanges();
            return role.RoleId;
        }

        public bool CheckPermissionUser(string userName, int PermissionId)
        {
            var UserId=context.Users.First(x => x.UserName == userName).UserId;
            var UserRoles=context.UserRoles.Where(r=> r.UserId == UserId).Select(r=> r.RoleId);
            var RolePermission=context.RolePermissions.Where(p=> p.PermissionId == PermissionId).Select(r=> r.RoleId);
            return RolePermission.Any(r => UserRoles.Contains(r));
        }

        public void EditRole(Role role)
        {
            context.Roles.Update(role);
            context.SaveChanges();
        }

        public void EditRolePermission(int roleId, List<int> selectedPermission)
        {
            context.RolePermissions.Where(r => r.RoleId == roleId).ToList().ForEach(r => context.RolePermissions.Remove(r));
            context.SaveChanges();
            foreach (var permission in selectedPermission)
            {
                context.RolePermissions.Add(new RolePermission() { RoleId = roleId, PermissionId = permission });
            }
            context.SaveChanges();
        }

        public List<Permission> GetAllPermission()
        {
            return context.Permissions.ToList();
        }

        public Role GetRoleById(int roleId)
        {
            return context.Roles.Find(roleId);
        }

        public List<int> GetRolePermissin(int roleId)
        {
            return context.RolePermissions.Where(r=> r.RoleId==roleId).Select(r => r.PermissionId).ToList();
        }
    }
}
