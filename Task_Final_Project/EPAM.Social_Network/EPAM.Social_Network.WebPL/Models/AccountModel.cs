using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.Dependencies;
using EPAM.Social_Network.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Security;

namespace EPAM.Social_Network.WebPL.Models
{
    public static class AccountModel
    {
        private static IDbLogic<Account> _accountDbLogic;
        private static IDbLogic<Role> _roleDbLogic;
        private static IDbLogic<Profile> _profileDbLogic;
        private static IFriendDbLogic _friendDbLogic;
        private static IMessageDbLogic _messageDbLogic;

        static AccountModel()
        {
            _accountDbLogic = DependencyResolver.AccountDbLogic;
            _roleDbLogic = DependencyResolver.RoleDbLogic;
            _profileDbLogic = DependencyResolver.ProfileDbLogic;
            _friendDbLogic = DependencyResolver.FriendDbLogic;
            _messageDbLogic = DependencyResolver.MessageDbLogic;
        }

        public static bool AddFriend()
        {
            var ctx = HttpContext.Current;
            var friendIdStr = ctx.Request["friendId"];
            var accIdStr = ctx.Request["accId"];

            if (int.TryParse(accIdStr, out int accId)
                && int.TryParse(friendIdStr, out int friendId))
            {
                var friend = _friendDbLogic.GetById(friendId, accId);

                if (friend != null)
                {
                    friend.IsAccept = true;
                    _friendDbLogic.Update(friend);

                    friend = _friendDbLogic.GetById(accId, friendId);

                    if (friend == null)
                    {
                        friend = new Friend
                        {
                            AccountId = accId,
                            FriendId = friendId,
                            IsAccept = true
                        };

                        return _friendDbLogic.Add(friend);
                    }
                    else if (!friend.IsAccept)
                    {
                        friend.IsAccept = true;
                        return _friendDbLogic.Update(friend);
                    }
                }
            }

            return false;
        }

        public static bool AddFriendRequest()
        {
            var ctx = HttpContext.Current;
            var friendIdStr = ctx.Request["friendId"];
            var authAccIdStr = ctx.Request["authAccId"];

            if (int.TryParse(authAccIdStr, out int authAccId)
                && int.TryParse(friendIdStr, out int friendId))
            {
                var friend = new Friend { AccountId = authAccId, FriendId = friendId };

                return _friendDbLogic.Add(friend);
            }

            return false;
        }

        public static bool DenyFriendRequest()
        {
            var ctx = HttpContext.Current;
            var friendIdStr = ctx.Request["friendId"];
            var accIdStr = ctx.Request["accId"];

            if (int.TryParse(accIdStr, out int accId)
                && int.TryParse(friendIdStr, out int friendId))
            {
                var friend = _friendDbLogic.GetById(friendId, accId);

                if (friend != null)
                {
                    return _friendDbLogic.Delete(friendId, accId);
                }
            }

            return false;
        }

        public static Account GetAccount(int accountId)
        {
            return _accountDbLogic.GetById(accountId);
        }

        public static Account GetAccountByLogin(string login)
        {
            return _accountDbLogic.GetAll()
                                  .FirstOrDefault(a => a.Login == login);
        }

        public static IEnumerable<Account> GetAccounts()
        {
            return _accountDbLogic.GetAll().ToArray();
        }

        public static Friend GetFriend(int accountId, int friendId)
        {
            return _friendDbLogic.GetById(accountId, friendId);
        }

        public static IEnumerable<Account> GetFriendsAccount(int accountId)
        {
            var friendsId = _friendDbLogic.GetAll()
                          .Where(f => f.AccountId == accountId && f.IsAccept)
                          .Select(f => f.FriendId)
                          .ToArray();

            return _accountDbLogic.GetAll()
                                  .Where(acc => friendsId.Contains(acc.Id))
                                  .ToArray();
        }

        public static IEnumerable<Account> GetFriendRequests(int accountId)
        {
            var friendRequestsId = _friendDbLogic.GetAll()
                          .Where(friend => friend.FriendId == accountId && !friend.IsAccept)
                          .Select(friend => friend.AccountId)
                          .ToArray();

            return _accountDbLogic.GetAll()
                                  .Where(acc => friendRequestsId.Contains(acc.Id))
                                  .ToArray();
        }

        public static IEnumerable<Message> GetCorrespondence(int accId, int friendId)
        {
            var accMsgs = _messageDbLogic.GetAll()
                                         .Where(m => m.AccountFromId == accId
                                                && m.AccountToId == friendId)
                                         .OrderBy(m => m.DateOfCreation)
                                         .ToArray();

            var friendMsgs = _messageDbLogic.GetAll()
                                         .Where(m => m.AccountFromId == friendId
                                                && m.AccountToId == accId)
                                         .OrderBy(m => m.DateOfCreation)
                                         .ToArray();

            if (accMsgs != null && friendMsgs != null)
            {
                var messages = accMsgs.Concat(friendMsgs)
                                      .OrderBy(m => m.DateOfCreation);

                return messages;
            }
            else if (accMsgs.Any() && !friendMsgs.Any())
            {
                return accMsgs;
            }
            else if (!accMsgs.Any() && friendMsgs.Any())
            {
                return friendMsgs;
            }
            else
            {
                return new Message[0];
            }
        }

        public static Profile GetProfileById(int profileId)
        {
            return _profileDbLogic.GetById(profileId);
        }

        public static IEnumerable<Profile> GetProfiles()
        {
            return _profileDbLogic.GetAll();
        }

        public static string GetRoleName(int? roleId)
        {
            if (roleId != null)
            {
                var role = _roleDbLogic.GetById(roleId.Value);
                return role.Name;
            }

            return "<No Role>";
        }

        public static IEnumerable<Role> GetRoles()
        {
            return _roleDbLogic.GetAll();
        }

        public static bool Register(string login, string password)
        {
            Account account = _accountDbLogic.GetAll()
                .FirstOrDefault(a => a.Login == login);

            if (account == null)
            {
                Role role;
                Profile profile = new Profile { FirstName = login };

                int profileId = _profileDbLogic.Add(profile);

                if (!_accountDbLogic.GetAll().Any())
                {
                    role = _roleDbLogic.GetAll().FirstOrDefault(r => r.Name == "Admin");
                }
                else
                {
                    role = _roleDbLogic.GetAll().FirstOrDefault(r => r.Name == "User");
                }

                account = new Account
                {
                    Login = login,
                    PasswordHash = Crypto.HashPassword(password),
                    ProfileId = profileId,
                    RoleId = role?.Id
                };

                _accountDbLogic.Add(account);

                return true;
            }

            return false;
        }

        public static void SaveChanges()
        {
            var ctx = HttpContext.Current;

            var profileIdStr = ctx.Request["profileId"];

            if (int.TryParse(profileIdStr, out int profId))
            {
                var profile = _profileDbLogic.GetById(profId);

                if (profile != null)
                {
                    var firstName = ctx.Request["FirstName"];
                    var middleName = ctx.Request["MiddleName"];
                    var lastName = ctx.Request["LastName"];
                    var male = ctx.Request["Male"];
                    var dateStr = ctx.Request["DateOfBirth"];
                    var city = ctx.Request["City"];

                    if (firstName != null
                        && !string.IsNullOrWhiteSpace(firstName))
                    {
                        profile.FirstName = firstName;
                    }

                    profile.MiddleName = middleName;
                    profile.LastName = lastName;
                    profile.Male = male;
                    profile.City = city;

                    if (string.IsNullOrWhiteSpace(dateStr))
                    {
                        profile.DateOfBirth = null;
                    }
                    else if (DateTime.TryParse(dateStr, out DateTime dateOfBirth))
                    {
                        profile.DateOfBirth = dateOfBirth;
                    }

                    WebImage profilePhoto = WebImage.GetImageFromRequest();

                    if (profilePhoto != null)
                    {
                        profile.ProfilePhoto = profilePhoto.GetBytes();
                    }
                    else if (ctx.Request["delImgHidden"] == "true")
                    {
                        profile.ProfilePhoto = null;
                    }

                    _profileDbLogic.Update(profile);
                }
            }

            var accIdStr = ctx.Request["accId"];

            if (accIdStr != null && int.TryParse(accIdStr, out int accountId))
            {
                var acc = _accountDbLogic.GetById(accountId);

                if (acc != null)
                {
                    var newPass = ctx.Request["newPass"];
                    var confirmPass = ctx.Request["confirmPass"];

                    if (newPass != null && confirmPass != null
                        && !string.IsNullOrWhiteSpace(newPass)
                        && !string.IsNullOrWhiteSpace(confirmPass)
                        && newPass == confirmPass)
                    {
                        acc.PasswordHash = Crypto.HashPassword(newPass);
                    }

                    var roleStr = ctx.Request["Role"];

                    if (int.TryParse(roleStr, out int roleId))
                    {
                        acc.RoleId = roleId;
                    }

                    _accountDbLogic.Update(acc);
                }
            }

            ctx.Response.Redirect("/Pages/Accounts/UserProfile");
        }

        public static bool SendMessage(Message message)
        {
            return _messageDbLogic.Add(message);
        }

        public static bool SignIn(string login, string password)
        {
            Account account = _accountDbLogic.GetAll()
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

        public static void RemoveAccount()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["profId"], out int profId))
            {
                Account acc = _accountDbLogic.GetAll()
                                             .FirstOrDefault(a => a.ProfileId == profId);

                if (acc != null && _accountDbLogic.Delete(acc.Id)
                    && _profileDbLogic.Delete(profId))
                {
                    _friendDbLogic.Delete(acc.Id);
                    _messageDbLogic.Delete(acc.Id);

                    httpContext.Response.Redirect("/Pages/Index");
                }

                httpContext.Response.AppendHeader("ErrorMsg", $"The User to delete was not found.");
                return;
            }

            httpContext.Response.AppendHeader("ErrorMsg", $"Incorrect data");
        }

        public static void RemoveFromFriends()
        {
            var ctx = HttpContext.Current;
            var authAccidStr = ctx.Request["authAccId"];
            var accFriendIdStr = ctx.Request["accFriendId"];

            if (int.TryParse(authAccidStr, out int authAccId)
                && int.TryParse(accFriendIdStr, out int accFriendId))
            {
                _friendDbLogic.Delete(authAccId, accFriendId);
                _friendDbLogic.Delete(accFriendId, authAccId);

                _messageDbLogic.Delete(authAccId, accFriendId);
                _messageDbLogic.Delete(accFriendId, authAccId);
            }
        }
    }
}