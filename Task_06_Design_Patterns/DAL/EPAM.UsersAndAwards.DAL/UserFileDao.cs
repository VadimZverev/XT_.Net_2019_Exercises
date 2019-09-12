using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class UserFileDao : IUserFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, User> _repoUsers;

        static UserFileDao()
        {
            _dataBase =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Users"]);

            _repoUsers = new Dictionary<int, User>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                if (File.Exists(_dataBase))
                {
                    string data = File.ReadAllText(_dataBase);

                    var usersDb = new { Users = new List<User>() };

                    usersDb = JsonConvert.DeserializeAnonymousType(data, usersDb);

                    foreach (User user in usersDb.Users)
                    {
                        _repoUsers.Add(user.Id, user);
                    }
                }
            }
        }

        public void Add(User user)
        {
            var lastId = _repoUsers.Any()
                ? _repoUsers.Keys.Max()
                : 0;
            user.Id = ++lastId;

            _repoUsers.Add(user.Id, user);
        }

        public bool Delete(int id)
        {
            return _repoUsers.Remove(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repoUsers.Values;
        }

        public User GetById(int id)
        {
            return _repoUsers.TryGetValue(id, out var user)
                   ? user
                   : null;
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                var users = from u in _repoUsers.Values
                            select new
                            {
                                u.Id,
                                u.Name,
                                u.DateOfBirth,
                                u.Age
                            };

                var db = new { Users = users };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
        }

        public bool Update(User user)
        {
            if (!_repoUsers.ContainsKey(user.Id))
            {
                return false;
            }

            _repoUsers[user.Id] = user;
            return true;
        }
    }
}