using Microsoft.AspNetCore.Http;
using MyLearn.Core.Dtos;
using MyLearn.Core.Dtos.Admin.Users;
using MyLearn.Core.Dtos.UserPanel;
using MyLearn.DataLayer.Entities.User;
using MyLearn.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLearn.Core.Service.UserService
{
    public interface IUserService
    {
        #region Register
        bool IsExistUserName(string userName);
        bool IsExistEamil(string email);
        User RegisterUser(RegisterViewModel registerViewModel);
    
        #endregion
        #region ActiveAccount
        User GerUserByActiveCode(string code);
        bool ActiveAccount(User user);
        #endregion
        #region Login
        User GetUserForLogin(string eamil, string password);
        #endregion
        #region ForgotPaasword
        User GetUserByEamil(string email);
        void UpdateUser(User user);
        #endregion
        #region UserPanel
        SideBarUserPanelViewModel GetSideBarUserPanel(string userName);
        UserPanelViewModel GetDataForUserPanel(string userName);
        void EditProfileForUserPanel(EditProfileViewModel editProfile,string userName);
        EditProfileViewModel GetDataForEditProdileUserPanel(string userName);
        void ChangePasswordViewModel(string userName,ChangePasswordForUserPanel changePassword);
        #endregion
        #region Wallet
        int GetBallanceUserWallet(string userName);
        int AddWallet(string userName,int amount,string descreption,int type);
        List<Wallet> GetWallets(string userName);
        Wallet GetWalletById(int id);
        void UpdateWallet(Wallet wallet);
        #endregion
        #region Admin
        Tuple<List<UserForAdminViewModel>,int,int> GetUsersForAdmin(string userName="",string Email = "",int page=1);
        int AddUserForAdmin(CreatUserViewModel creatUserViewModel);
        void EditUserForAdmin(EditUserViewModel editUserViewModel);
        EditUserViewModel GetUserForEditAdmin(int userId);
        List<Role> GetAllRoles();
        void AddUserRole(int userId, List<int> Roles);
        void EditUsrRole(int userId, List<int> Roles);
        List<int> GetUserRoles(int userId);
        void DeleteUserForAdmin(int userId);

        #endregion
        User GetUserByUserName(string userName);
        int GetUserIdByUserName(string userName);
        User GetUserByUserId(int userId);
        bool InUserUsed(int courseId, string userName);

    }
}
