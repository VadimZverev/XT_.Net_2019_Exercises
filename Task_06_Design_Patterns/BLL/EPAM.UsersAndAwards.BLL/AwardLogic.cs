using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.BLL
{
    public class AwardLogic : IAwardLogic
    {
        private readonly IAwardDao _awardDao;

        public AwardLogic(IAwardDao awardDao)
        {
            _awardDao = awardDao;
        }

        public AwardLogic(IAwardFileDao awardFileDao)
        {
            _awardDao = awardFileDao;
        }

        public bool Add(Award award)
        {
            Award _award = _awardDao.GetAll()
                .FirstOrDefault(a => a.Title == award.Title);

            if (_award == null)
            {
                _awardDao.Add(award);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            return _awardDao.Delete(id);
        }

        public bool Delete(string title)
        {
            Award award = _awardDao.GetAll()
                                   .FirstOrDefault(a => a.Title == title);

            if (award == null)
                return false;

            return _awardDao.Delete(award.Id);
        }

        public IEnumerable<Award> GetAll()
        {
            return _awardDao.GetAll();
        }

        public Award GetById(int id)
        {
            return _awardDao.GetById(id);
        }

        public void Save()
        {
            var _awardFileDao = _awardDao as IAwardFileDao;

            if (_awardDao != null)
            {
                _awardFileDao.Save();
            }
        }

        public bool Update(Award award)
        {
            return _awardDao.Update(award);
        }
    }
}
