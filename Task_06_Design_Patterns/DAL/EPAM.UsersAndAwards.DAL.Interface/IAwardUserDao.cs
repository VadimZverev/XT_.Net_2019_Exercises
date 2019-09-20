using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IAwardUserDao
    {
        void Add(AwardUser awardUser);

        bool Delete(int awardId, int userId);

        IEnumerable<AwardUser> GetAll();

        AwardUser GetById(int id);

        AwardUser GetById(int awardId, int userId);

        bool Update(AwardUser awardUser);
    }
}
