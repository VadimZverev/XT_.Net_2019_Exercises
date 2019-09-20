using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.BLL
{
    public class AccountLogic : IAccountLogic
    {
        private readonly IAccountDao _accountDao;

        public AccountLogic(IAccountDao accountDao)
        {
            _accountDao = accountDao;
        }

        public bool Add(Account account)
        {
            Account _account = _accountDao.GetAll()
                .FirstOrDefault(acc => acc.Id == account.Id 
                                || acc.Login == account.Login);

            if (_account == null)
            {
                _accountDao.Add(account);
                return true;
            }

            return false;
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

        public bool Update(Account account)
        {
            return _accountDao.Update(account);
        }
    }
}
