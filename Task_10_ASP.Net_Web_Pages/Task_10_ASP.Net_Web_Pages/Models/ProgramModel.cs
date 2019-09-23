using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.Common;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Configuration;

namespace Task_10_ASP.Net_Web_Pages.Models
{
    public static class ProgramModel
    {
        private static readonly string _storageMode
            = WebConfigurationManager.AppSettings["StorageMode"];

        private static IUserLogic _userLogic;
        private static IAwardLogic _awardLogic;
        private static IAwardUserLogic _awardUserLogic;

        public static void AddAward()
        {
            var httpContext = HttpContext.Current;
            string title = httpContext.Request["Title"];
            string redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";

            if (!string.IsNullOrWhiteSpace(title))
            {
                Award award = new Award { Title = title };

                WebImage photo = WebImage.GetImageFromRequest();

                if (photo != null)
                {
                    award.Image = photo.GetBytes();
                }

                if (!_awardLogic.Add(award))
                {
                    httpContext.Response.AppendHeader("ErrorMsg", $"{award.Title} already exist");
                    return;
                }

                httpContext.Response.Redirect(redirect);
            }

            httpContext.Response.AppendHeader("ErrorMsg", "Input must be not empty or only white spaces");
        }

        public static void AddUser()
        {
            var httpContext = HttpContext.Current;
            string redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
            string name = httpContext.Request["Name"] ?? null;
            string strDateOfBirth = httpContext.Request["DateOfBirth"] ?? null;

            if (DateTime.TryParse(strDateOfBirth, out DateTime dateOfBirth)
                && !string.IsNullOrWhiteSpace(name))
            {
                User user = new User { Name = name, DateOfBirth = dateOfBirth };

                WebImage photo = WebImage.GetImageFromRequest();

                if (photo != null)
                {
                    user.Image = photo.GetBytes();
                }

                if (!_userLogic.Add(user))
                {
                    httpContext.Response.AppendHeader("ErrorMsg", $"{user.Name} already exist");
                    return;
                }

                httpContext.Response.Redirect(redirect);
            }

            httpContext.Response.AppendHeader("ErrorMsg", "input must be not only white spaces or incorrect data");
        }

        public static bool AwardExistByUserId(int userId)
        {
            return _awardUserLogic.GetAll().Any(au => au.UserId == userId);
        }

        public static Award GetAward(int idAward)
        {
            return _awardLogic.GetById(idAward);
        }

        public static IEnumerable<Award> GetAwards()
        {
            return _awardLogic.GetAll();
        }

        public static IEnumerable<Award> GetAwardsByUserId(int userId)
        {
            var awardsId = _awardUserLogic.GetAll()
                                          .Where(au => au.UserId == userId)
                                          .Select(a => a.AwardId).ToArray();

            var awards = _awardLogic.GetAll().Where(a => awardsId.Contains(a.Id));

            foreach (var award in awards)
            {
                yield return award;
            }
        }

        public static User GetUser(int userId)
        {
            return _userLogic.GetById(userId);
        }

        public static IEnumerable<User> GetUsers()
        {
            return _userLogic.GetAll();
        }

        public static void InitialLogic()
        {
            if (_storageMode == "File")
            {
                _userLogic = DependencyResolver.UserFileLogic;
                _awardLogic = DependencyResolver.AwardFileLogic;
                _awardUserLogic = DependencyResolver.AwardUserFileLogic;
            }
            else if (_storageMode == "Database")
            {
                _userLogic = DependencyResolver.UserDbLogic;
                _awardLogic = DependencyResolver.AwardDbLogic;
                _awardUserLogic = DependencyResolver.AwardUserDbLogic;
            }
            else
            {
                _userLogic = DependencyResolver.UserLogic;
                _awardLogic = DependencyResolver.AwardLogic;
                _awardUserLogic = DependencyResolver.AwardUserLogic;
            }
        }

        public static void RemoveAward()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["awardId"], out int awardId))
            {
                Award award = _awardLogic.GetById(awardId);

                if (award == null)
                {
                    httpContext.Response.AppendHeader("ErrorMsg", $"The Award to delete was not found.");
                    return;
                }

                if (_awardLogic.Delete(award.Id))
                {
                    var awardUserList = _awardUserLogic.GetAll()
                                                       .Where(au => au.AwardId == award.Id)
                                                       .ToList();

                    if (awardUserList.Count != 0)
                    {
                        foreach (var awardUser in awardUserList)
                        {
                            _awardUserLogic.Delete(awardUser.AwardId, awardUser.UserId);
                        }
                    }

                    return;
                }
            }

            httpContext.Response.AppendHeader("ErrorMsg", $"Incorrect data");
        }

        public static void RemoveUser()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["userId"], out int userId))
            {
                User user = _userLogic.GetById(userId);

                if (user != null && _userLogic.Delete(user.Id))
                {
                    var awardUserList = _awardUserLogic.GetAll()
                                                       .Where(au => au.UserId == user.Id)
                                                       .ToList();

                    if (awardUserList.Count != 0)
                    {
                        foreach (var awardUser in awardUserList)
                        {
                            _awardUserLogic.Delete(awardUser.AwardId, awardUser.UserId);
                        }
                    }

                    return;
                }

                httpContext.Response.AppendHeader("ErrorMsg", $"The User to delete was not found.");
                return;
            }

            httpContext.Response.AppendHeader("ErrorMsg", $"Incorrect data");
        }

        public static void SelectAction(HttpContext context)
        {
            switch (context.Request.UrlReferrer.AbsolutePath)
            {
                case "/Pages/AddAward":
                    AddAward();
                    break;
                case "/Pages/AddUser":
                    AddUser();
                    break;
                case "/Pages/ShowAwards" when context.Request["Mode"].Contains("Delete"):
                    RemoveAward();
                    break;
                case "/Pages/ShowUsers" when context.Request["Mode"].Contains("Delete"):
                    RemoveUser();
                    break;
                case "/Pages/ShowAwards" when context.Request["Mode"].Contains("Update"):
                    context.Response.Redirect($"/Pages/UpdateAward?awardId={context.Request["awardId"]}");
                    break;
                case "/Pages/ShowUsers" when context.Request["Mode"].Contains("Update"):
                    context.Response.Redirect($"/Pages/UpdateUser?userId={context.Request["userId"]}");
                    break;
                case "/Pages/UpdateAward" when context.Request.HttpMethod.Contains("POST"):
                    UpdateAward();
                    break;
                case "/Pages/UpdateUser" when context.Request.HttpMethod.Contains("POST"):
                    UpdateUser();
                    break;
            }
        }

        public static void UpdateAward()
        {
            var httpContext = HttpContext.Current;
            string returnUrl = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
            string Title = httpContext.Request["Title"];

            if (int.TryParse(httpContext.Request["awardId"], out int awardId)
                && !string.IsNullOrWhiteSpace(Title))
            {
                Award award = _awardLogic.GetById(awardId);

                if (award != null)
                {
                    award = new Award
                    {
                        Id = award.Id,
                        Title = Title,
                        Image = award.Image
                    };

                    WebImage photo = WebImage.GetImageFromRequest();

                    if (photo != null)
                    {
                        award.Image = photo.GetBytes();
                    }
                    else if (httpContext.Request["delImage"] == "on")
                    {
                        award.Image = null;
                    }

                    if (_awardLogic.Update(award))
                    {
                        httpContext.Response.Redirect(returnUrl);
                    }
                }
            }

            httpContext.Response.Headers.Add("returnUrl", returnUrl);
            httpContext.Response.Headers.Add("ErrorMsg", "Failed to update award");
        }

        public static void UpdateUser()
        {
            var httpContext = HttpContext.Current;
            string name = httpContext.Request["Name"] ?? null;
            string strDateOfBirth = httpContext.Request["DateOfBirth"] ?? null;
            string strIdUser = httpContext.Request["userId"];

            if (int.TryParse(strIdUser, out int userId)
                && DateTime.TryParse(strDateOfBirth, out DateTime dateOfBirth)
                && !string.IsNullOrWhiteSpace(name))
            {
                User user = _userLogic.GetById(userId);

                if (user != null)
                {
                    user = new User
                    {
                        Id = user.Id,
                        Name = name,
                        DateOfBirth = dateOfBirth,
                        Image = user.Image
                    };

                    WebImage photo = WebImage.GetImageFromRequest();

                    if (photo != null)
                    {
                        user.Image = photo.GetBytes();
                    }
                    else if (httpContext.Request["delImage"] == "on")
                    {
                        user.Image = null;
                    }
                }

                if (_userLogic.Update(user))
                {
                    var awardsIds = httpContext.Request.Params.GetValues("awardsSelects");
                    AddDeleteAwardUser(awardsIds, userId);

                    string redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
                    httpContext.Response.Redirect(redirect);
                }
            }

            httpContext.Response.Headers.Add("ErrorMsg", "Failed to update user");
        }

        private static void AddDeleteAwardUser(string[] awardIds, int userId)
        {
            if (awardIds == null)
            {
                var userAwardsIdAll = _awardUserLogic.GetAll()
                                                   .Where(au => au.UserId == userId)
                                                   .Select(au => au.AwardId)
                                                   .ToArray();

                foreach (var awardId in userAwardsIdAll)
                {
                    _awardUserLogic.Delete(awardId, userId);
                }
            }
            else
            {
                List<int> awardIdList = new List<int>();

                foreach (var awardId in awardIds)
                {
                    if (int.TryParse(awardId, out int id))
                    {
                        awardIdList.Add(id);
                    }
                }

                var awardUserList = _awardUserLogic.GetAll()
                                                   .Where(au => au.UserId == userId)
                                                   .Select(au => au.AwardId)
                                                   .ToList();

                var awardIdToDel = awardUserList.Except(awardIdList);

                foreach (var awardId in awardIdToDel)
                {
                    _awardUserLogic.Delete(awardId, userId);
                }

                foreach (var awardId in awardIdList)
                {
                    if (!awardUserList.Contains(awardId))
                    {
                        var au = new AwardUser() { AwardId = awardId, UserId = userId };
                        _awardUserLogic.Add(au);
                    }
                }
            }
        }
    }
}