using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class AwardFakeDao : IAwardDao
    {
        private static readonly Dictionary<int, Award> _repoAwards = new Dictionary<int, Award>();

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
