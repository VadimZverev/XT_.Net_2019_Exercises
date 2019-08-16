using System.Collections.Generic;
using System.Linq;
using Users_and_Awards.Entities;

namespace Users_and_Awards.DAL
{
    public class AwardUsersStorage : IAwardUserRepository
    {
        public AwardUsersStorage()
        {
            AwardUsers = new List<AwardUser>();
        }

        public AwardUsersStorage(ICollection<AwardUser> awardUsers)
        {
            AwardUsers = awardUsers.ToList();
        }

        private List<AwardUser> AwardUsers { get; set; }

        public bool AddAwardUser(AwardUser awardUser)
        {
            if (!AwardUsers.Exists(
                ua => ua.AwardId == awardUser.AwardId
                      && ua.UserId == awardUser.UserId))
            {
                AwardUsers.Add(awardUser);
                return true;
            }

            return false;
        }

        public ICollection<AwardUser> GetAllAwardUser() => AwardUsers;

        public AwardUser GetAwardUser(string awardId, string userId) =>
            AwardUsers.FirstOrDefault(
                au => au.AwardId.ToString() == awardId
                      && au.UserId.ToString() == userId);

        public bool RemoveAwardUser(string awardId, string userId)
        {
            AwardUser awardUser = GetAwardUser(awardId, userId);

            if (awardUser == null)
                return false;

            return AwardUsers.Remove(awardUser);
        }
    }
}
