using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using EPAM.Social_Network.Loggers;
using System;
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
            try
            {
                bool isExist = _accountDao.GetAll()
                                          .Any(acc => acc.Id == entity.Id
                                                      || acc.Login == entity.Login);

                if (!isExist)
                {
                    return _accountDao.Add(entity);
                }
            }
            catch (Exception ex)
            {
                string message = "Failed to add account to DB.";
                Logger.SendError(ex, message);
            }

            return 0;
        }

        public bool Delete(int id)
        {
            try
            {
                return _accountDao.Delete(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete account from DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public IEnumerable<Account> GetAll()
        {
            try
            {
                return _accountDao.GetAll();
            }
            catch (Exception ex)
            {
                string message = "Failed to get accounts from DB.";
                Logger.SendError(ex, message);
            }

            return new Account[0];
        }

        public Account GetById(int id)
        {
            try
            {
                return _accountDao.GetById(id);
            }
            catch (Exception ex)
            {
                string message = "Failed to get account from DB.";
                Logger.SendError(ex, message);
            }

            return null;
        }

        public bool Update(Account entity)
        {
            try
            {
                return _accountDao.Update(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to update account.";
                Logger.SendError(ex, message);
            }

            return false;
        }
    }
}
