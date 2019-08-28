﻿using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.BLL
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;

        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }

        public UserLogic(IUserFileDao userFileDao)
        {
            _userDao = userFileDao;
        }

        public bool Add(User user)
        {
            User _user = _userDao.GetById(user.Id);

            if (_user == null)
            {
                _userDao.Add(user);
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            return _userDao.Delete(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _userDao.GetAll();
        }

        public User GetById(int id)
        {
            return _userDao.GetById(id);
        }

        public bool Update(User user)
        {
            return _userDao.Update(user);
        }

        public void Save()
        {
            var _userFileDao = _userDao as IUserFileDao;

            if (_userDao != null)
            {
                _userFileDao.Save();
            }
        }
    }
}