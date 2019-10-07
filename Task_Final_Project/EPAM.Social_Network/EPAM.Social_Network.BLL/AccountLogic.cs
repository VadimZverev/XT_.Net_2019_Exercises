using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.Social_Network.BLL
{
    public class AccountLogic : IDbLogic<Account>
    {
        private readonly IDbDao<Account> _accountDao;

        public AccountLogic(IDbDao<Account> accountDao)
        {
            _accountDao = accountDao;
        }

        public int Add(Account entity)
        {
            bool isExist = _accountDao.GetAll()
                                      .Any(acc => acc.Id == entity.Id
                                                  || acc.Login == entity.Login);

            if (!isExist)
            {
                return _accountDao.Add(entity);
            }

            return 0;
        }

        public bool Delete(int id)
        {
            return _accountDao.Delete(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _accountDao.GetAll();
        }

        public Account GetById(int id)
        {
            return _accountDao.GetById(id);
        }

        public bool Update(Account entity)
        {
            return _accountDao.Update(entity);
        }
    }
}
