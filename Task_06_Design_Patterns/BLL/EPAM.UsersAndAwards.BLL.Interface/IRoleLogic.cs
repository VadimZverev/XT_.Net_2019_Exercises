using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IRoleLogic
    {
        bool Add(Role role);

        bool Delete(int id);

        IEnumerable<Role> GetAll();

        Role GetById(int id);

        bool Update(Role role);
    }
}
