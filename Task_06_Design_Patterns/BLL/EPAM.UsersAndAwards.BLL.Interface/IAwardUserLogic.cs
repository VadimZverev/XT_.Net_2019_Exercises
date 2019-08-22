using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IAwardUserLogic
    {
        bool Add(AwardUser awardUser);

        bool Delete(int id);

        IEnumerable<AwardUser> GetAll();

        AwardUser GetById(int id);

        void Save();

        bool Update(AwardUser awardUser);
    }
}
