using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IAwardUserDao
    {
        void Add(AwardUser awardUser);

        bool Delete(int id);

        IEnumerable<AwardUser> GetAll();

        AwardUser GetById(int id);

        bool Update(AwardUser awardUser);
    }
}
