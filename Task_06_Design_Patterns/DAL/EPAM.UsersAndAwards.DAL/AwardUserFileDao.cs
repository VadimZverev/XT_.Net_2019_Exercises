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
    public class AwardUserFileDao : IAwardUserFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, AwardUser> _repoAwardUsers;

        static AwardUserFileDao()
        {
            _dataBase =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["AwardUser"]);

            _repoAwardUsers = new Dictionary<int, AwardUser>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && File.Exists(_dataBase))
            {
                string data = File.ReadAllText(_dataBase);

                var awardUserDb = new { AwardUser = new List<AwardUser>() };

                awardUserDb = JsonConvert.DeserializeAnonymousType(data, awardUserDb);

                foreach (AwardUser awardUser in awardUserDb.AwardUser)
                {
                    if (int.TryParse($"{awardUser.AwardId}{awardUser.UserId}", out int id))
                    {
                        _repoAwardUsers.Add(id, awardUser);
                    }
                }

            }
        }

        public void Add(AwardUser awardUser)
        {
            int.TryParse($"{awardUser.AwardId}{awardUser.UserId}", out int id);

            _repoAwardUsers.Add(id, awardUser);

            Save();
        }

        public bool Delete(int awardId, int userId)
        {
            var keyStr = $"{awardId}{userId}";

            if (int.TryParse(keyStr, out int key)
                && _repoAwardUsers.Remove(key))
            {
                Save();
                return true;
            }

            return false;
        }

        public IEnumerable<AwardUser> GetAll()
        {
            foreach (var item in _repoAwardUsers.Values)
            {
                yield return new AwardUser
                {
                    AwardId = item.AwardId,
                    UserId = item.UserId
                };
            }
        }

        public AwardUser GetById(int awardId, int userId)
        {
            var keyStr = $"{awardId}{userId}";

            if (int.TryParse(keyStr, out int key))
            {
                return _repoAwardUsers.TryGetValue(key, out var awardUser)
                    ? new AwardUser { AwardId = awardUser.AwardId, UserId = awardUser.UserId }
                    : null;
            }

            return null;
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && !string.IsNullOrWhiteSpace(_dataBase))
            {
                var awardUser = from au in _repoAwardUsers.Values
                                select new
                                {
                                    au.AwardId,
                                    au.UserId
                                };

                var db = new { AwardUser = awardUser };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
        }
    }
}
