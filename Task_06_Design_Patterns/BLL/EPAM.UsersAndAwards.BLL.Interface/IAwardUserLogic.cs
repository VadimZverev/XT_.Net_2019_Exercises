using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IAwardUserLogic
    {
        bool Add(AwardUser awardUser);

        bool Delete(int awardId, int userId);

        IEnumerable<AwardUser> GetAll();

        AwardUser GetById(int id);

        AwardUser GetById(int awardId, int userId);

        bool Update(AwardUser awardUser);
    }
}
