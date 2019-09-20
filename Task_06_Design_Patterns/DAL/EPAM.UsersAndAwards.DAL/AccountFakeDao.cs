using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using System.Collections.Generic;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class AccountFakeDao : IAccountDao
    {
        private static readonly Dictionary<int, Account> _repoAccounts
            = new Dictionary<int, Account>();

        public void Add(Account account)
        {
            var lastId = _repoAccounts.Any()
                ? _repoAccounts.Keys.Max()
                : 0;
            account.Id = ++lastId;

            _repoAccounts.Add(account.Id, account);
        }

        public bool Delete(int id)
        {
            return _repoAccounts.Remove(id);
        }

        public IEnumerable<Account> GetAll()
        {
            return _repoAccounts.Values;
        }

        public Account GetById(int id)
        {
            return _repoAccounts.TryGetValue(id, out var account)
                ? account
                : null;
        }

        public bool Update(Account account)
        {
            if (!_repoAccounts.ContainsKey(account.Id))
            {
                return false;
            }

            _repoAccounts[account.Id] = account;
            return true;
        }
    }
}
