using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.BLL
{
    public class AwardUserLogic : IAwardUserLogic
    {
        private readonly IAwardUserDao _awardUserDao;

        public AwardUserLogic(IAwardUserDao awardUserDao)
        {
            _awardUserDao = awardUserDao;
        }

        public AwardUserLogic(IAwardUserFileDao awardUserDao)
        {
            _awardUserDao = awardUserDao;
        }

        public bool Add(AwardUser awardUser)
        {
            AwardUser _awardUser = 
                _awardUserDao.GetAll()
                             .FirstOrDefault(au => au.AwardId == awardUser.AwardId
                                                   && au.UserId == awardUser.UserId);

            if (_awardUser == null)
            {
                _awardUserDao.Add(awardUser);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            return _awardUserDao.Delete(id);
        }

        public IEnumerable<AwardUser> GetAll()
        {
            return _awardUserDao.GetAll();
        }

        public AwardUser GetById(int id)
        {
            return _awardUserDao.GetById(id);
        }

        public void Save()
        {
            var _awardUserFileDao = _awardUserDao as IAwardUserFileDao;

            if (_awardUserDao != null)
            {
                _awardUserFileDao.Save();
            }
        }

        public bool Update(AwardUser awardUser)
        {
            return _awardUserDao.Update(awardUser);
        }
    }
}
