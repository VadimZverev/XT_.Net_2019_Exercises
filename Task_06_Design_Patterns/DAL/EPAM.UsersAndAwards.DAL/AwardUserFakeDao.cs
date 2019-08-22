using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL
{
    public class AwardUserFakeDao : IAwardUserDao
    {
        private static readonly Dictionary<int, AwardUser> _repoAwardUsers = new Dictionary<int, AwardUser>();

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
