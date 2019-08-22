using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class AwardFileDao : IAwardFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, Award> _repoAwards;

        static AwardFileDao()
        {
            _dataBase = ConfigurationManager.AppSettings["Awards"];
            _repoAwards = new Dictionary<int, Award>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                if (File.Exists(_dataBase))
                {
                    string data = File.ReadAllText(_dataBase);

                    var awardsDb = new { Awards = new List<Award>() };

                    awardsDb = JsonConvert.DeserializeAnonymousType(data, awardsDb);

                    foreach (Award award in awardsDb.Awards)
                    {
                        _repoAwards.Add(award.Id, award);
                    }
                }
            }
        }

        public void Add(Award award)
        {
            var lastId = _repoAwards.Any()
                ? _repoAwards.Keys.Max()
                : 0;
            award.Id = ++lastId;

            _repoAwards.Add(award.Id, award);
        }

        public bool Delete(int id)
        {
            return _repoAwards.Remove(id);
        }

        public IEnumerable<Award> GetAll()
        {
            return _repoAwards.Values;
        }

        public Award GetById(int id)
        {
            return _repoAwards.TryGetValue(id, out var award)
                ? award
                : null;
        }

        public void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                var awards = from a in _repoAwards.Values
                             select new
                             {
                                 a.Id,
                                 a.Title
                             };

                var db = new { Awards = awards };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
        }

        public bool Update(Award award)
        {
            if (!_repoAwards.ContainsKey(award.Id))
            {
                return false;
            }

            _repoAwards[award.Id] = award;
            return true;
        }
    }
}
