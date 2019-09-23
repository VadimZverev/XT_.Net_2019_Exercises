using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IRoleDao
    {
        void Add(Role role);

        bool Delete(int id);

        IEnumerable<Role> GetAll();

        Role GetById(int id);

        bool Update(Role role);
    }
}
