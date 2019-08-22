using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IAwardDao
    {
        void Add(Award award);

        bool Delete(int id);

        IEnumerable<Award> GetAll();

        Award GetById(int id);

        bool Update(Award award);
    }
}
