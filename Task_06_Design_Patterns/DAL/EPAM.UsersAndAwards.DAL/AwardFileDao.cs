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
    public class AwardFileDao : IAwardFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, Award> _repoAwards;

        static AwardFileDao()
        {
            _dataBase =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Awards"]);

            _repoAwards = new Dictionary<int, Award>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && File.Exists(_dataBase))
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

        public void Add(Award award)
        {
            var lastId = _repoAwards.Any()
                ? _repoAwards.Keys.Max()
                : 0;

            award.Id = ++lastId;

            _repoAwards.Add(award.Id, award);

            Save();
        }

        public bool Delete(int id)
        {
            if (_repoAwards.Remove(id))
            {
                Save();
                return true;
            }

            return false;
        }

        public IEnumerable<Award> GetAll()
        {
            foreach (var award in _repoAwards.Values)
            {
                yield return new Award
                {
                    Id = award.Id,
                    Title = award.Title,
                    Image = award.Image
                };
            }
        }

        public Award GetById(int id)
        {
            return _repoAwards.TryGetValue(id, out var award)
                ? new Award
                {
                    Id = award.Id,
                    Title = award.Title,
                    Image = award.Image
                }
                : null;
        }

        private void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && !string.IsNullOrWhiteSpace(_dataBase))
            {
                var awards = from a in _repoAwards.Values
                             select new
                             {
                                 a.Id,
                                 a.Title,
                                 a.Image
                             };

                var db = new { Awards = awards };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
        }

        public bool Update(Award award)
        {
            if (_repoAwards.ContainsKey(award.Id))
            {
                _repoAwards[award.Id] = award;

                Save();

                return true;
            }

            return false;
        }
    }
}