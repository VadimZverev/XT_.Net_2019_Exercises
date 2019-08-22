using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using Newtonsoft.Json;
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
            _dataBase = ConfigurationManager.AppSettings["AwardUser"];
            _repoAwardUsers = new Dictionary<int, AwardUser>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                if (File.Exists(_dataBase))
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
        }

        public void Add(AwardUser awardUser)
        {
            int.TryParse($"{awardUser.AwardId}{awardUser.UserId}", out int id);

            _repoAwardUsers.Add(id, awardUser);
        }

        public bool Delete(int id)
        {
            return _repoAwardUsers.Remove(id);
        }

        public IEnumerable<AwardUser> GetAll()
        {
            return _repoAwardUsers.Values;
        }

        public AwardUser GetById(int id)
        {
            return _repoAwardUsers.TryGetValue(id, out var awardUSer)
                ? awardUSer
                : null;
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase))
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

        public bool Update(AwardUser awardUser)
        {
            if (!_repoAwardUsers.ContainsKey(awardUser.AwardId))
            {
                return false;
            }

            _repoAwardUsers[awardUser.AwardId] = awardUser;
            return true;
        }
    }
}
