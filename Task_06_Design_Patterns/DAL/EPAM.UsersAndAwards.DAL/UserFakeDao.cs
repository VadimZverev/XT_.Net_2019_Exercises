using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class UserFakeDao : IUserDao
    {
        private static readonly Dictionary<int, User> _repoUsers = new Dictionary<int, User>();

        public void Add(User user)
        {
            var lastId = _repoUsers.Any()
                ? _repoUsers.Keys.Max()
                : 0;
            user.Id = ++lastId;

            _repoUsers.Add(user.Id, user);
        }

        public bool Delete(int id)
        {
            return _repoUsers.Remove(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _repoUsers.Values;
        }

        public User GetById(int id)
        {
            return _repoUsers.TryGetValue(id, out var user)
                   ? user
                   : null;
        }

        public bool Update(User user)
        {
            if (!_repoUsers.ContainsKey(user.Id))
            {
                return false;
            }

            _repoUsers[user.Id] = user;
            return true;
        }
    }
}
