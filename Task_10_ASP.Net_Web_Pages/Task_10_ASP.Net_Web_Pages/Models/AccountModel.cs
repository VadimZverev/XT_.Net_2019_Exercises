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

        public static void ChageRole()
        {
            var context = HttpContext.Current;
            string returnUrl = context.Request["returnUrl"] ?? "Pages/Index";
            string strRole = context.Request["Role"] ?? "1";
            string accId = context.Request["accId"] ?? null;

            if (int.TryParse(strRole, out int role)
                && int.TryParse(accId, out int id))
            {
                var acc = _accountLogic.GetById(id);

                if (acc != null)
                {
                    acc.Role = role;
                    context.Response.Redirect(returnUrl);
                }
                else
                {
                    context.Response.AppendHeader("ErrorMsg", "Account not found.");
                    return;
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
                account = new Account
                {
                    Login = login,
                    Password = Crypto.HashPassword(password),
                    Role = (int)Roles.User
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
            }
            else if (_storageMode == "Database")
            {
                //TODO: написать DBLogic
            }
        }

        public static bool SignIn(string login, string password)
        {
            Account account = _accountLogic.GetAll()
                .FirstOrDefault(a => a.Login == login);

            if (account != null
                && Crypto.VerifyHashedPassword(account.Password, password))
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

        public static IEnumerable<Account> GetAccounts()
        {
            return _accountLogic.GetAll();
        }
    }
}