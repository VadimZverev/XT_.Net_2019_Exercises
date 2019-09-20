using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.BLL.Interface
{
    public interface IAccountLogic
    {
        bool Add(Account account);

        bool Delete(int id);

        IEnumerable<Account> GetAll();

        Account GetById(int id);

        bool Update(Account account);

    }
}
