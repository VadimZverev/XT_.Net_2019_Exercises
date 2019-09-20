using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IAwardLogic
    {
        bool Add(Award award);

        bool Delete(int id);

        bool Delete(string title);

        IEnumerable<Award> GetAll();

        Award GetById(int id);

        bool Update(Award award);
    }
}
