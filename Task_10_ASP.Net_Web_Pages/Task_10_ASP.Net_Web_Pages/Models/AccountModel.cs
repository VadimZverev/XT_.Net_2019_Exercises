using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.Common;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Configuration;
using System.Web.Security;

namespace Task_10_ASP.Net_Web_Pages.Models
{
    public static class AccountModel
    {
        private static readonly string _storageMode
            = WebConfigurationManager.AppSettings["StorageMode"];

        private static IAccountLogic _accountLogic;
        private static IRoleLogic _roleLogic;

        public static void ChageRole()
        {
            var context = HttpContext.Current;
            string returnUrl = context.Request["returnUrl"] ?? "Pages/Index";
            string strRoleId = context.Request["Role"] ?? "1";
            string accId = context.Request["accId"] ?? null;

            if (int.TryParse(strRoleId, out int roleId)
                && int.TryParse(accId, out int id))
            {
                var acc = _accountLogic.GetById(id);

                if (acc == null)
                {
                    context.Response.AppendHeader("ErrorMsg", "Account not found.");
                    return;
                }
                else
                {
                    acc.RoleId = roleId;

                    if (_accountLogic.Update(acc))
                    {
                        context.Response.Redirect(returnUrl);
                    }
                    else
                    {
                        context.Response.AppendHeader("ErrorMsg", "Failed to update Account.");
                        return;
                    }
                }
            }

            context.Response.AppendHeader("ErrorMsg", "Incorrect data.");
        }

        public static bool Create(string login, string password)
        {
            Account account = _accountLogic.GetAll()
                .FirstOrDefault(a => a.Login == login);

            if (account == null)
            {
                Role role;

                if (!_accountLogic.GetAll().Any())
                {
                    role = _roleLogic.GetAll().FirstOrDefault(r => r.Name == "Admin");
                }
                else
                {
                    role = _roleLogic.GetAll().FirstOrDefault(r => r.Name == "User");
                }

                account = new Account
                {
                    Login = login,
                    PasswordHash = Crypto.HashPassword(password),
                    RoleId = role?.Id
                };

                return _accountLogic.Add(account);
            }

            return false;
        }

        public static void InitialLogic()
        {
            if (_storageMode == "File")
            {
                _accountLogic = DependencyResolver.AccountFileLogic;
                _roleLogic = DependencyResolver.RoleFileLogic;
            }
            else if (_storageMode == "Database")
            {
                _accountLogic = DependencyResolver.AccountDbLogic;
                _roleLogic = DependencyResolver.RoleDbLogic;
            }
            else
            {
                _accountLogic = DependencyResolver.AccountLogic;
                _roleLogic = DependencyResolver.RoleLogic;
            }
        }

        public static bool SignIn(string login, string password)
        {
            Account account = _accountLogic.GetAll()
                .FirstOrDefault(a => a.Login == login);

            if (account != null
                && Crypto.VerifyHashedPassword(account.PasswordHash, password))
            {
                FormsAuthentication.SetAuthCookie(login, true);
                return true;
            }

            return false;
        }

        public static void SignOut()
        {
            var context = HttpContext.Current;
            FormsAuthentication.SignOut();

            if (context.Request?.UrlReferrer?.AbsolutePath != null)
            {
                context.Response.Redirect(context.Request.UrlReferrer.AbsolutePath);
            }
            else
            {
                context.Response.Redirect("/Pages/Index");
            }
        }

        public static Account GetAccount(int accountId)
        {
            return _accountLogic.GetById(accountId);
        }

        public static string GetRoleName(int? roleId)
        {
            if (roleId != null)
            {
                var role = _roleLogic.GetById(roleId.Value);
                return role.Name;
            }

            return "<No Role>";
        }

        public static IEnumerable<Role> GetRoles()
        {
            return _roleLogic.GetAll();
        }

        public static IEnumerable<Account> GetAccounts()
        {
            return _accountLogic.GetAll();
        }
    }
}