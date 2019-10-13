using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;
using System.Linq;

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
            Friend friend = _friendDao.GetById(entity.AccountId, entity.FriendId);

            if (friend == null)
            {
                return _friendDao.Add(entity);
            }

            return false;
        }

        public void Delete(int accountId)
        {
            _friendDao.Delete(accountId);
        }

        public bool Delete(int accountId, int friendId)
        {
            return _friendDao.Delete(accountId, friendId);
        }

        public IEnumerable<Friend> GetAll()
        {
            return _friendDao.GetAll();
        }

        public Friend GetById(int accountId, int friendId)
        {
            return _friendDao.GetById(accountId, friendId);
        }

        public bool Update(Friend entity)
        {
            return _friendDao.Update(entity);
        }
    }
}
