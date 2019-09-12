using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.Common;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
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

    public static class ProgramManager
    {
        private static string _storageMode
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

        public static bool AddAward(string title)
        {
            return _awardLogic.Add(new Award { Title = title });
        }

        public static bool AddAwardUser(int idUser, int idAward)
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
                return true;
            }

            return false;
        }

        public static bool AddUser(string name, DateTime dateOfBirth)
        {
            User user = new User { Name = name, DateOfBirth = dateOfBirth };

            return _userLogic.Add(user);
        }

        public static bool RemoveAward(int idAward)
        {
            Award award = _awardLogic.GetById(idAward);

            if (award != null)
            {
                _awardLogic.Delete(award.Id);

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

                return true;
            }

            return false;
        }

        public static bool RemoveAwardUser(int idUser, int idAward)
        {
            Award award = _awardLogic.GetById(idAward);
            User user = _userLogic.GetById(idUser);

            if (int.TryParse($"{ award.Id}{user.Id}", out int idAwardUser))
            {
                if (_awardUserLogic.Delete(idAwardUser))
                {
                    user.Awards.Remove(award);
                    award.Users.Remove(user);

                    return true;
                }

                return false;
            }

            return false;
        }

        public static bool RemoveUser(int idUser)
        {
            User user = _userLogic.GetById(idUser);

            if (user != null)
            {
                _userLogic.Delete(user.Id);

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

                return true;
            }

            return false;
        }

        public static IEnumerable<Award> GetAwards()
        {
            return _awardLogic.GetAll();
        }

        public static IEnumerable<Award> GetUserAwards(int idUser)
        {
            User user = _userLogic.GetById(idUser);

            return user?.Awards;
        }

        public static IEnumerable<User> GetUsers()
        {
            return _userLogic.GetAll();
        }
    }
}