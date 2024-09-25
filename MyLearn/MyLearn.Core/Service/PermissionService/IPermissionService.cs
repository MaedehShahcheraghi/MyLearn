using MyLearn.DataLayer.Entities.Permission;
using MyLearn.DataLayer.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.PermissionService
{
    public interface IPermissionService
    {

        #region Permission
        List<Permission> GetAllPermission();
        List<int> GetRolePermissin(int roleId);
        int AddRole(Role role);
        void AddPermissionRole(int roleId,List<int> SelectedPermission);
        void EditRole(Role role);
        Role GetRoleById(int roleId);
        void EditRolePermission(int roleId, List<int> selectedPermission);
        bool CheckPermissionUser(string userName, int PermissionId);

        #endregion
    }
}
