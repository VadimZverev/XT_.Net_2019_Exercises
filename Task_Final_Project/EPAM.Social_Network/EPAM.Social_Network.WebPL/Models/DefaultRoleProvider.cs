using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.Dependencies;
using EPAM.Social_Network.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace EPAM.Social_Network.WebPL.Models
{
    public class DefaultRoleProvider : RoleProvider
    {
        private readonly IDbLogic<Account> _accountDbLogic;
        private readonly IDbLogic<Role> _roleDbLogic;

        public DefaultRoleProvider()
        {
            _accountDbLogic = DependencyResolver.AccountDbLogic;
            _roleDbLogic = DependencyResolver.RoleDbLogic;

            SetStartRole();
        }

        private void SetStartRole()
        {
            var roles = _roleDbLogic.GetAll()
                                  .Where(r => r.Name == "Admin"
                                           || r.Name == "User")
                                  .ToArray();

            if (!roles.Any(r => r.Name == "Admin"))
            {
                _roleDbLogic.Add(new Role() { Name = "Admin" });
            }
            if (!roles.Any(r => r.Name == "User"))
            {
                _roleDbLogic.Add(new Role() { Name = "User" });
            }
        }

        public override string[] GetAllRoles()
        {
            return _roleDbLogic.GetAll()
                             .Select(r => r.Name)
                             .ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var account = _accountDbLogic.GetAll()
                .FirstOrDefault(acc => acc.Login == username);

            if (account.RoleId != null)
            {
                var role = _roleDbLogic.GetAll().FirstOrDefault(r => r.Id == account.RoleId);

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