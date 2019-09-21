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

        public bool Delete(int awardId, int userId)
        {
            var keyStr = $"{awardId}{userId}";

            if (int.TryParse(keyStr, out int key))
            {
                return _repoAwardUsers.Remove(key);
            }

            return false;
        }

        public IEnumerable<AwardUser> GetAll()
        {
            return _repoAwardUsers.Values;
        }

        public AwardUser GetById(int awardId, int userId)
        {
            var keyStr = $"{awardId}{userId}";

            if (int.TryParse(keyStr, out int key))
            {
                return _repoAwardUsers.TryGetValue(key, out var awardUser)
                    ? awardUser
                    : null;
            }

            return null;
        }
    }
}
