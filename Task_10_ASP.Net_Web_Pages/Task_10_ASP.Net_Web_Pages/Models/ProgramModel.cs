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
    public enum SaveMode
    {
        AwardMode = 0,
        UserMode = 1,
        AwardUserMode = 2,
        AllMode = 4
    }

    public static class ProgramModel
    {
        private static readonly string _storageMode
            = WebConfigurationManager.AppSettings["StorageMode"];

        private static IUserLogic _userLogic;
        private static IAwardLogic _awardLogic;
        private static IAwardUserLogic _awardUserLogic;

        public static void Save(SaveMode mode = SaveMode.AllMode)
        {
            if (_storageMode == "File")
            {
                switch (mode)
                {
                    case SaveMode.AwardMode:
                        _awardLogic.Save();
                        break;
                    case SaveMode.UserMode:
                        _userLogic.Save();
                        break;
                    case SaveMode.AwardUserMode:
                        _awardUserLogic.Save();
                        break;
                    case SaveMode.AllMode:
                        _userLogic.Save();
                        _awardLogic.Save();
                        _awardUserLogic.Save();
                        break;
                }
            }
        }

        public static void InitialLogic()
        {
            if (_storageMode == "File")
            {
                _userLogic = DependencyResolver.UserFileLogic;
                _awardLogic = DependencyResolver.AwardFileLogic;
                _awardUserLogic = DependencyResolver.AwardUserFileLogic;

                if (_awardUserLogic.GetAll().Count() != 0)
                {
                    foreach (var user in _userLogic.GetAll())
                    {
                        var userAwards = _awardUserLogic.GetAll().Where(au => au.UserId == user.Id);

                        foreach (var item in userAwards)
                        {
                            var award = _awardLogic.GetById(item.AwardId);
                            user.Awards.Add(award);
                        }
                    }

                    foreach (var award in _awardLogic.GetAll())
                    {
                        var userAwards = _awardUserLogic.GetAll().Where(au => au.AwardId == award.Id);

                        foreach (var item in userAwards)
                        {
                            var user = _userLogic.GetById(item.UserId);
                            award.Users.Add(user);
                        }
                    }
                }
            }
            else
            {
                _userLogic = DependencyResolver.UserLogic;
                _awardLogic = DependencyResolver.AwardLogic;
                _awardUserLogic = DependencyResolver.AwardUserLogic;
            }
        }

        public static void AddAward()
        {
            var httpContext = HttpContext.Current;
            string title = httpContext.Request["Title"];
            string redirect;

            if (!string.IsNullOrWhiteSpace(title))
            {
                Award award = new Award { Title = title };

                WebImage photo = WebImage.GetImageFromRequest();

                if (photo != null)
                {
                    award.Image = photo.GetBytes();
                }

                if (_awardLogic.Add(award))
                {
                    redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
                    httpContext.Response.Redirect(redirect);
                }
                else
                {
                    redirect = httpContext.Request.UrlReferrer.AbsolutePath + "?ErrorMsg=Award exist";
                    httpContext.Response.Redirect(redirect);
                }
            }

            redirect = httpContext.Request.UrlReferrer.AbsolutePath
                + "?ErrorMsg=input must be not empty or only white spaces";

            httpContext.Response.Redirect(redirect);
        }

        public static void AddAwardUser()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["User"], out int idUser)
                && int.TryParse(httpContext.Request["Award"], out int idAward))
            {
                Award award = _awardLogic.GetById(idAward);
                User user = _userLogic.GetById(idUser);

                AwardUser awardUser = new AwardUser
                {
                    AwardId = award.Id,
                    UserId = user.Id
                };

                if (_awardUserLogic.Add(awardUser))
                {
                    user.Awards.Add(award);
                    award.Users.Add(user);

                    httpContext.Response.Redirect("/Pages/Index");
                }

                httpContext.Response.AppendHeader("ErrorMsg", $"{award.Title} already added {user.Name}");
            }
        }

        public static void AddUser()
        {
            string redirect;
            var httpContext = HttpContext.Current;

            string name = httpContext.Request["Name"];
            if (DateTime.TryParse(httpContext.Request["DateOfBirth"], out DateTime dateOfBirth)
                && !string.IsNullOrWhiteSpace(name))
            {
                User user = new User { Name = name, DateOfBirth = dateOfBirth };

                WebImage photo = WebImage.GetImageFromRequest();

                if (photo != null)
                {
                    user.Image = photo.GetBytes();
                }

                if (_userLogic.Add(user))
                {
                    redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
                    httpContext.Response.Redirect(redirect);
                }
                else
                {
                    redirect = httpContext.Request.UrlReferrer.AbsolutePath + "?ErrorMsg=User exist";
                    httpContext.Response.Redirect(redirect);
                }

            }

            redirect = httpContext.Request.UrlReferrer.AbsolutePath
                + "?ErrorMsg=input must be not only white spaces";

            httpContext.Response.Redirect(redirect);
        }

        public static Award GetAward(int idAward)
        {
            return _awardLogic.GetById(idAward);
        }

        public static IEnumerable<Award> GetAwards()
        {
            return _awardLogic.GetAll();
        }

        public static User GetUser(int idUser)
        {
            return _userLogic.GetById(idUser);
        }

        public static IEnumerable<User> GetUsers()
        {
            return _userLogic.GetAll();
        }

        public static IEnumerable<Award> GetUserAwards(int idUser)
        {
            User user = _userLogic.GetById(idUser);

            return user?.Awards;
        }

        public static void RemoveAward()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["idAward"], out int idAward))
            {
                Award award = _awardLogic.GetById(idAward);

                if (award != null && _awardLogic.Delete(award.Id))
                {
                    var users = _userLogic.GetAll().Where(u => u.Awards.Contains(award));

                    foreach (User user in users)
                    {
                        user.Awards.Remove(award);
                    }

                    var awardUserList = _awardUserLogic.GetAll()
                                                       .Where(au => au.AwardId == award.Id)
                                                       .ToList();

                    foreach (var awardUser in awardUserList)
                    {
                        if (int.TryParse($"{awardUser.AwardId}{awardUser.UserId}", out int id))
                        {
                            _awardUserLogic.Delete(id);
                        }
                    }
                }
            }
        }

        public static void RemoveAwardUser()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["User"], out int idUser)
                && int.TryParse(httpContext.Request["Award"], out int idAward))
            {
                Award award = _awardLogic.GetById(idAward);
                User user = _userLogic.GetById(idUser);

                if (int.TryParse($"{award.Id}{user.Id}", out int idAwardUser)
                    && _awardUserLogic.Delete(idAwardUser))
                {
                    user.Awards.Remove(award);
                    award.Users.Remove(user);
                }
            }

            httpContext.Response.Redirect("/Pages/Index");
        }

        public static void RemoveUser()
        {
            var httpContext = HttpContext.Current;

            if (int.TryParse(httpContext.Request["idUser"], out int idUser))
            {
                User user = _userLogic.GetById(idUser);

                if (user != null && _userLogic.Delete(user.Id))
                {
                    var awards = _awardLogic.GetAll().Where(a => a.Users.Contains(user));

                    foreach (Award award in awards)
                    {
                        award.Users.Remove(user);
                    }

                    var awardUserList = _awardUserLogic.GetAll()
                                                       .Where(au => au.UserId == user.Id)
                                                       .ToList();

                    foreach (var awardUser in awardUserList)
                    {
                        if (int.TryParse($"{awardUser.AwardId}{awardUser.UserId}", out int id))
                        {
                            _awardUserLogic.Delete(id);
                        }
                    }
                }
            }
        }

        public static void SelectAction(HttpContext context)
        {
            switch (context.Request.UrlReferrer.AbsolutePath)
            {
                case "/Pages/AddAward":
                    AddAward();
                    break;
                case "/Pages/AddAwardToUser":
                    AddAwardUser();
                    break;
                case "/Pages/AddUser":
                    AddUser();
                    break;
                case "/Pages/ShowAwards" when context.Request["Mode"].Contains("Delete"):
                    RemoveAward();
                    break;
                case "/Pages/RemoveAwardToUser":
                    RemoveAwardUser();
                    break;
                case "/Pages/ShowUsers" when context.Request["Mode"].Contains("Delete"):
                    RemoveUser();
                    break;
                case "/Pages/ShowAwards" when context.Request["Mode"].Contains("Update"):
                    context.Response.Redirect($"/Pages/UpdateAward?idAward={context.Request["idAward"]}");
                    break;
                case "/Pages/ShowUsers" when context.Request["Mode"].Contains("Update"):
                    context.Response.Redirect($"/Pages/UpdateUser?idUser={context.Request["idUser"]}");
                    break;
                case "/Pages/UpdateAward" when context.Request.HttpMethod.Contains("POST"):
                    UpdateAward();
                    break;
                case "/Pages/UpdateUser" when context.Request.HttpMethod.Contains("POST"):
                    UpdateUser();
                    break;
            }
        }

        public static void UpdateUser()
        {
            var httpContext = HttpContext.Current;
            string name = httpContext.Request["Name"];

            if (int.TryParse(httpContext.Request["IdUser"], out int idUser)
                && DateTime.TryParse(httpContext.Request["DateOfBirth"], out DateTime dateOfBirth)
                && !string.IsNullOrWhiteSpace(name))
            {
                User user = _userLogic.GetById(idUser);

                if (user != null)
                {
                    user.Name = name;
                    user.DateOfBirth = dateOfBirth;

                    WebImage photo = WebImage.GetImageFromRequest();

                    if (photo != null)
                    {
                        user.Image = photo.GetBytes();
                    }
                    else if (httpContext.Request["delImage"] == "on" )
                    {
                        user.Image = null;
                    }

                    string redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
                    httpContext.Response.Redirect(redirect);
                }
            }

            httpContext.Response.Headers.Add("ErrorMsg", "Failed to update user");
        }

        public static void UpdateAward()
        {
            var httpContext = HttpContext.Current;
            string Title = httpContext.Request["Title"];

            if (int.TryParse(httpContext.Request["IdAward"], out int idAward)
                && !string.IsNullOrWhiteSpace(Title))
            {
                Award award = _awardLogic.GetById(idAward);

                if (award != null)
                {
                    WebImage photo = WebImage.GetImageFromRequest();

                    if (photo != null)
                    {
                        award.Image = photo.GetBytes();
                    }
                    else if (httpContext.Request["delImage"] == "on")
                    {
                        award.Image = null;
                    }

                    award.Title = Title;

                    string redirect = httpContext.Request?["returnUrl"] ?? "/Pages/Index";
                    httpContext.Response.Redirect(redirect);
                }
            }

            httpContext.Response.Headers.Add("ErrorMsg", "Failed to update award");
        }
    }
}