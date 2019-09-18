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
        private static IUserLogic _userLogic;

        public static bool Create(string login, string password)
        {
            Account account = _accountLogic.GetAll()
                .FirstOrDefault(a => a.Login == login && a.Password == password);

            if (account == null)
            {
                var user = new User { Name = login };
                _userLogic.Add(user);

                account = new Account
                {
                    Login = login,
                    Password = password,
                    Role = (int)Roles.User,
                    UserId = user.Id,
                    User = user
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
                _userLogic = DependencyResolver.UserFileLogic;

                foreach (var account in _accountLogic.GetAll())
                {
                    account.User = _userLogic.GetById(account.UserId);
                }
            }
            else
            {
                //TODO: написать мемори сторедж
            }
        }

        public static bool SignIn(string login, string password)
        {
            Account account = _accountLogic.GetAll()
                .FirstOrDefault(a => a.Login == login && a.Password == password);

            if (account != null)
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

        public static Account GetAccount(int userId)
        {
            return _accountLogic.GetAll()
                .FirstOrDefault(acc => acc.UserId == userId);
        }
    }
}