using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using EPAM.Social_Network.Loggers;
using System;
using System.Collections.Generic;

namespace EPAM.Social_Network.BLL
{
    public class FriendLogic : IFriendDbLogic
    {
        private readonly IFriendDbDao _friendDao;

        public FriendLogic(IFriendDbDao friendDao)
        {
            _friendDao = friendDao;
        }

        public bool Add(Friend entity)
        {
            try
            {
                Friend friend = _friendDao.GetById(entity.AccountId, entity.FriendId);

                if (friend == null)
                {
                    return _friendDao.Add(entity);
                }
            }
            catch (Exception ex)
            {
                string message = "Failed to add friend to DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public void Delete(int accountId)
        {
            try
            {
                _friendDao.Delete(accountId);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete friends of account from DB.";
                Logger.SendError(ex, message);
            }
        }

        public bool Delete(int accountId, int friendId)
        {
            try
            {
                return _friendDao.Delete(accountId, friendId);
            }
            catch (Exception ex)
            {
                string message = "Failed to delete friend from DB.";
                Logger.SendError(ex, message);
            }

            return false;
        }

        public IEnumerable<Friend> GetAll()
        {
            try
            {
                return _friendDao.GetAll();
            }
            catch (Exception ex)
            {
                string message = "Failed to get friends from DB.";
                Logger.SendError(ex, message);
            }

            return new Friend[0];

        }

        public Friend GetById(int accountId, int friendId)
        {
            try
            {
                return _friendDao.GetById(accountId, friendId);
            }
            catch (Exception ex)
            {
                string message = "Failed to get friend from DB.";
                Logger.SendError(ex, message);
            }

            return null;
        }

        public bool Update(Friend entity)
        {
            try
            {
                return _friendDao.Update(entity);
            }
            catch (Exception ex)
            {
                string message = "Failed to update friend.";
                Logger.SendError(ex, message);
            }

            return false;
        }
    }
}
