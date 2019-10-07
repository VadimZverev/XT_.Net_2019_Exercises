using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.Social_Network.BLL
{
    public class ProfileLogic : IDbLogic<Profile>
    {
        private readonly IDbDao<Profile> _profileDao;

        public ProfileLogic(IDbDao<Profile> profileDao)
        {
            _profileDao = profileDao;
        }

        public int Add(Profile entity)
        {
            Profile profile = _profileDao.GetById(entity.Id);

            if (profile == null)
            {
                return _profileDao.Add(entity);
            }

            return 0;
        }

        public bool Delete(int id)
        {
            return _profileDao.Delete(id);
        }

        public IEnumerable<Profile> GetAll()
        {
            return _profileDao.GetAll();
        }

        public Profile GetById(int id)
        {
            return _profileDao.GetById(id);
        }

        public bool Update(Profile entity)
        {
            return _profileDao.Update(entity);
        }
    }
}
