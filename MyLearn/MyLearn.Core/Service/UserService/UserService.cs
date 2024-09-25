using Microsoft.AspNetCore.Http;
using MyLearn.Core.Dtos;
using MyLearn.Core.Dtos.Admin.Users;
using MyLearn.Core.Dtos.UserPanel;
using MyLearn.Core.Generators;
using MyLearn.Core.Security;
using MyLearn.DataLayer.Context;
using MyLearn.DataLayer.Entities.User;
using MyLearn.DataLayer.Entities.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.Convertors;

namespace MyLearn.Core.Service.UserService
{
    public class UserService : IUserService
    {
        private readonly MyLearnContext context;

        public UserService(MyLearnContext context)
        {
            this.context = context;
        }

        public bool ActiveAccount(User user)
        {
            try
            {
                user.IsActive = true;
                user.ActiveCode = NameGenerator.GenerateUnicCode();
                context.SaveChanges();
                return true;

            }
            catch 
            {
                return false;
            }

        }

        public User GerUserByActiveCode(string code)
        {
            return context.Users.Where(c => c.ActiveCode == code).FirstOrDefault();
        }

        public bool IsExistEamil(string email)
        {
            return context.Users.Any(u => u.Email == email);
        }

        public User GetUserForLogin(string eamil, string password)
        {
           return context.Users.FirstOrDefault(u=> u.Email == eamil && u.Password ==PasswordHelper.EncodePasswordMd5(password)
          );
        }

        public bool IsExistUserName(string userName)
        {
                return context.Users.Any(u=> u.UserName == userName);
        }

        public User RegisterUser(RegisterViewModel registerViewModel)
        {
            var register=new User()
            {
                UserName = registerViewModel.UserName,
                Email = FixedText.FixedEmail(registerViewModel.Email),
                Password = PasswordHelper.EncodePasswordMd5(registerViewModel.Password),
                ActiveCode = NameGenerator.GenerateUnicCode(),
                IsActive = false,
                RegisterDate = DateTime.Now,
                IsDelete = false,
                AvatarName="Defult.jpg"
            };

            context.Users.Add(register);
            context.SaveChanges();
            return register;
        }

        public User GetUserByEamil(string email)
        {
          return context.Users.First(u => u.Email == FixedText.FixedEmail(email));
        }

        public void UpdateUser(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public SideBarUserPanelViewModel GetSideBarUserPanel(string userName)
        {
            return context.Users.Where(u => u.UserName == userName).Select(u => new SideBarUserPanelViewModel()
            {
                UserName = u.UserName,
                ImageName = u.AvatarName,
                RegisterDate = u.RegisterDate
            }).FirstOrDefault();
        }

        public UserPanelViewModel GetDataForUserPanel(string userName)
        {
           return context.Users.Where(u=>u.UserName==userName).Select(u => new UserPanelViewModel()
           {
               UserName=u.UserName,
               WalletSum=GetBallanceUserWallet(userName),
               Email=u.Email,
               RegisterDate=u.RegisterDate
           }).First();
        }

        public EditProfileViewModel GetDataForEditProdileUserPanel(string userName)
        {
            return context.Users.Where(u => u.UserName == userName && u.IsActive).Select(u => new EditProfileViewModel()
            {
                UserName = u.UserName,
                Email = u.Email,
                AvateName = u.AvatarName
            }).First();
        }

        public void EditProfileForUserPanel(EditProfileViewModel editProfile,string userName)
        {
           var user=GetUserByUserName(userName);
            if (editProfile.Avatar!=null) {
                if (editProfile.AvateName!="Defult.jpg")
                {
                    string currentpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editProfile.AvateName);
                    if (File.Exists(currentpath))
                    {
                        File.Delete(currentpath);
                    }
                }
                editProfile.AvateName = NameGenerator.GenerateUnicCode() + Path.GetExtension(editProfile.Avatar.FileName);
                string newpath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editProfile.AvateName);
                using (var stram=new FileStream(newpath,FileMode.Create))
                {
                    editProfile.Avatar.CopyTo(stram);
                }
            }
            user.UserName = editProfile.UserName;
            user.Email=FixedText.FixedEmail(editProfile.Email);
            user.AvatarName = editProfile.AvateName;
            context.SaveChanges();
        }

        public User GetUserByUserName(string userName)
        {
            return context.Users.First(u=> u.UserName==userName);   
        }

        public void ChangePasswordViewModel(string userName, ChangePasswordForUserPanel changePassword)
        {
            var user=GetUserByUserName(userName);
            if (user.Password == PasswordHelper.EncodePasswordMd5(changePassword.Password))
            {
                user.Password =PasswordHelper.EncodePasswordMd5(changePassword.NewPassword);
                context.SaveChanges();
            }
        }

        public int GetBallanceUserWallet(string userName)
        {
            var userId=GetUserIdByUserName(userName);
            var pay=context.Wallets.Where(context=>context.UserId == userId && context.TypeId == 1 && context.IsPay).Select(context=> context.Amount).ToList();
            var ballance=context.Wallets.Where(context=>context.UserId==userId && context.TypeId==2 && context.IsPay).Select(context=> context.Amount).ToList();

            return (pay.Sum()-ballance.Sum());
        }

     

        public int GetUserIdByUserName(string userName)
        {
            return context.Users.First(u=> u.UserName==userName).UserId;
        }

        public List<Wallet> GetWallets(string userName)
        {
            var userId = GetUserIdByUserName(userName);
            return context.Wallets.Where(w=> w.UserId==userId && w.IsPay).ToList();
        }

        public int AddWallet(string userName, int amount, string descreption,int type)
        {
            var wallet = new Wallet()
            {
                UserId = GetUserIdByUserName(userName),
                TypeId = type,
                Amount = amount,
                Descreption = descreption,
                CreateDate = DateTime.Now,
                IsPay=false

            };
            if (type==2)
            {
                wallet.IsPay = true;
            }
            context.Wallets.Add(wallet);
            context.SaveChanges();
            return wallet.WalletId;

        }

        public Wallet GetWalletById(int id)
        {
            return context.Wallets.Find(id);
        }

        public void UpdateWallet(Wallet wallet)
        {
           context.Wallets.Update(wallet);
            context.SaveChanges();
        }

        public Tuple<List<UserForAdminViewModel>, int,int> GetUsersForAdmin(string userName = "", string Email = "", int page = 1)
        {
            IQueryable<User> users = context.Users;
            if (!string.IsNullOrEmpty(userName))
            {
                users = users.Where(u => u.UserName == userName);
            }
            if (!string.IsNullOrEmpty(Email))
            {
                users = users.Where(u => u.Email == FixedText.FixedEmail(Email));
            }
            int take = 5;
            int skip = (page - 1) * take;
            int pagecount=users.Count()/take;
            if ((users.Count() % take) !=0)
            {
                pagecount++;
            }
            var userforadmin=users.Skip(skip).Take(take).Select(u=> new UserForAdminViewModel()
            {
                UserId=u.UserId,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                RegisterDate = u.RegisterDate
            }).ToList();
            return Tuple.Create(userforadmin, pagecount,page);
        }

        public int AddUserForAdmin(CreatUserViewModel creatUserViewModel)
        {

            var user = new User()
            {
                UserName = creatUserViewModel.UserName,
                Password = PasswordHelper.EncodePasswordMd5(creatUserViewModel.Password),
                RegisterDate = DateTime.Now,
                IsActive = true,
                ActiveCode = NameGenerator.GenerateUnicCode(),
                Email = FixedText.FixedEmail(creatUserViewModel.Email),
                IsDelete = false
            };
            if (creatUserViewModel.UserAvatar !=null)
            {
                var imageName = NameGenerator.GenerateUnicCode() +Path.GetExtension(creatUserViewModel.UserAvatar.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imageName);
                using(var stream=new FileStream(path, FileMode.Create))
                {
                    creatUserViewModel.UserAvatar.CopyTo(stream);
                }
                user.AvatarName = imageName;
            }
        
            context.Users.Add(user);
            context.SaveChanges();
            return user.UserId;

        }

        public void EditUserForAdmin(EditUserViewModel editUserViewModel)
        {
            var user=GetUserByUserId(editUserViewModel.UserId);
            user.UserName = editUserViewModel.UserName;
            user.Password=PasswordHelper.EncodePasswordMd5(editUserViewModel.Password);
            user.Email=FixedText.FixedEmail(editUserViewModel.Email);
            if (editUserViewModel.UserAvatar != null)
            {
                if (editUserViewModel.AvatarName!="Defult.jpg")
                {
                    var imagepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", editUserViewModel.AvatarName);
                    if (File.Exists(imagepath))
                    {
                        File.Delete(imagepath); 
                    }
                }
                var imageName = NameGenerator.GenerateUnicCode() + Path.GetExtension(editUserViewModel.UserAvatar.FileName);
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserAvatar", imageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    editUserViewModel.UserAvatar.CopyTo(stream);
                }

                user.AvatarName = imageName;
            }
            UpdateUser(user);

        }

        public User GetUserByUserId(int userId)
        {
            return context.Users.First(u=> u.UserId==userId);
        }

        public EditUserViewModel GetUserForEditAdmin(int userId)
        {
           return context.Users.Where(u=> u.UserId==userId).Select(u => new EditUserViewModel
           {
               UserName = u.UserName,
               Email = u.Email,
               AvatarName = u.AvatarName,
               UserId = u.UserId
           }).First();
        }

        public List<Role> GetAllRoles()
        {
            return context.Roles.ToList();
        }

        public void AddUserRole(int userId, List<int> Roles)
        {
            foreach (var item in Roles)
            {
                context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = item
                });
            }
            context.SaveChanges();

        }

        public void EditUsrRole(int userId, List<int> Roles)
        {
            context.UserRoles.Where(u=> u.UserId==userId).ToList().ForEach(u=> context.UserRoles.Remove(u));
            foreach (var item in Roles)
            {
                context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = item
                });
            }
            context.SaveChanges();
        }

        public List<int> GetUserRoles(int userId)
        {
            return context.UserRoles.Where(u=> u.UserId== userId).Select(u=> u.RoleId).ToList();
        }

        public void DeleteUserForAdmin(int userId)
        {
            var user=GetUserByUserId(userId);
            user.IsDelete = true;
            context.SaveChanges();
        }

        public bool InUserUsed(int courseId, string userName)
        {
            int userId=GetUserIdByUserName(userName);

            return context.UserCourses.Any(u=> u.UserId == userId && u.CourseId==courseId);
        }
    }
}
