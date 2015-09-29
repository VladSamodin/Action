using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models.User;
using System;
using System.Linq;
using System.Web.Helpers;
using System.Web.Security;

namespace MvcPL.Providers
{
    public class CustomMembershipProvider : MembershipProvider
    {
        
        private IService<BllRole> roleService
        {
            get
            {
                return (IService<BllRole>)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));
            }
        }
        

        private IUserService userService
        {
            get
            {
                return (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));
            }
        }

        public CustomMembershipProvider() { }


        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            BllUser user = userService.GetByPredicate(u => u.Email == email).FirstOrDefault();
            if (user == null)
            {
                return null;
            }
            else
            {
                //return new MembershipUser("CustomMembershipProvider", user.Email, null, user.Email, null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
                return ToMembershipUser(user);
            }
        }

        private MembershipUser ToMembershipUser(BllUser user)
        {
            return new MembershipUser("CustomMembershipProvider", user.Email, null, user.Email, null, null, false, false, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, DateTime.MinValue);
        }

        public MembershipUser CreateUser(UserCreateModel user)
        {
            if (GetUser(user.Email, false) != null)
            {
                return null;
            }
            else
            {
                BllUser newUser = userService.Create(user.ToBllUser());
                if (newUser == null)
                    return null;
                newUser = userService.GetByPredicate(u => u.Email == user.Email).First();
                BllRole userRole = roleService.GetById((int)user.Role);
                userService.AddRole(newUser, userRole);
                return ToMembershipUser(newUser);

                //return GetUser(user.Email, false);
            }
        }

        public MembershipUser UpdateUserData(UserEditModel user, String oldEmail)
        {
            BllUser bllUser = userService.GetByPredicate(u => u.Email == oldEmail).FirstOrDefault();
            if (bllUser == null)
            {
                return null;
            }
            else
            {
                bllUser.Email = user.Email;
                bllUser.Name = user.Name;
                if (!String.IsNullOrEmpty(user.Password))
                    bllUser.Password = user.Password;
                BllUser oldUser = userService.Update(bllUser);
                return oldUser == null ? null : ToMembershipUser(oldUser);

                //return GetUser(user.Email, false);
            }
        }

        public override bool ValidateUser(string email, string password)
        {
            BllUser user = userService.GetByPredicate(u => u.Email == email).FirstOrDefault();
            if (user != null && password == user.Password)
            {
                /*
                BllRole bllRole = roleService.GetByPredicate(r => r.Name == "User" && r.Id == user.RoleId).FirstOrDefault();
                if (bllRole != null)
                {
                    return true;
                }
                 * */
                return true;
            }

            return false;
        }

        public override bool DeleteUser(string email, bool deleteAllRelatedData)
        {
            var user = userService.GetByPredicate(u => u.Email == email).FirstOrDefault();
            if (user != null)
            {
                //((CustomRoleProvider)Roles.Provider).RemoveUsersFromRoles(new string[] { email }, new string[] { "User", "Administrator" });
                userService.Delete(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotImplementedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

    }
}