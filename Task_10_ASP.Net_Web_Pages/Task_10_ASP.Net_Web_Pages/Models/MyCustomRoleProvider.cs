using EPAM.UsersAndAwards.Common;
using EPAM.UsersAndAwards.Entities;
using EPAM.UsersAndAwards.BLL.Interface;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Task_10_ASP.Net_Web_Pages.Models
{
    public class MyCustomRoleProvider : RoleProvider
    {
        private readonly IAccountLogic _accountLogic;
        private readonly IRoleLogic _roleLogic;

        public MyCustomRoleProvider()
        {
            var _storeMode = WebConfigurationManager.AppSettings["StorageMode"];

            switch (_storeMode)
            {
                case "Database":
                    _accountLogic = DependencyResolver.AccountDbLogic;
                    _roleLogic = DependencyResolver.RoleDbLogic;
                    break;
                case "File":
                    _accountLogic = DependencyResolver.AccountFileLogic;
                    _roleLogic = DependencyResolver.RoleFileLogic;
                    break;
                default:
                    _accountLogic = DependencyResolver.AccountLogic;
                    _roleLogic = DependencyResolver.RoleLogic;
                    break;
            }

            SetStartRole();
        }

        private void SetStartRole()
        {
            var roles = _roleLogic.GetAll()
                                  .Where(r => r.Name == "Admin"
                                           || r.Name == "User")
                                  .ToArray();

            if (!roles.Any(r => r.Name == "Admin"))
            {
                _roleLogic.Add(new Role() { Name = "Admin" });
            }
            if (!roles.Any(r => r.Name == "User"))
            {
                _roleLogic.Add(new Role() { Name = "User" });
            }
        }

        public override string[] GetAllRoles()
        {
            return _roleLogic.GetAll()
                             .Select(r => r.Name)
                             .ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var account = _accountLogic.GetAll()
                .FirstOrDefault(acc => acc.Login == username);

            if (account.RoleId != null)
            {
                var role = _roleLogic.GetAll().FirstOrDefault(r => r.Id == account.RoleId);

                switch (role.Name)
                {
                    case "Admin":
                        return new[] { "Admin", "User" };
                    case "User":
                        return new[] { "User" };
                    default:
                        return new string[] { };
                }
            }
            else
            {
                return new string[] { };
            }
        }

        #region Not Used

        public override string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }


        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}