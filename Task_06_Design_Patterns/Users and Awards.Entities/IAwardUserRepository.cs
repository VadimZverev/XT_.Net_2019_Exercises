using System.Collections.Generic;

namespace Users_and_Awards.Entities
{
    public interface IAwardUserRepository
    {
        bool AddAwardUser(AwardUser awardUser);

        ICollection<AwardUser> GetAllAwardUser();

        AwardUser GetAwardUser(string awardId, string userId);

        bool RemoveAwardUser(string awardId, string userId);
    }
}
