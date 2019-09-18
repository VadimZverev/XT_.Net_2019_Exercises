using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.DAL.Interface
{
    public interface IAccountDao
    {
        void Add(Account account);

        bool Delete(int id);

        IEnumerable<Account> GetAll();

        Account GetById(int id);

        bool Update(Account account);
    }
}
