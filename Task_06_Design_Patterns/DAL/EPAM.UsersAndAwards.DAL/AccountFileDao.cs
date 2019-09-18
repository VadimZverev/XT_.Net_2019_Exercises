using EPAM.UsersAndAwards.DAL.Interface;
using EPAM.UsersAndAwards.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace EPAM.UsersAndAwards.DAL
{
    public class AccountFileDao : IAccountFileDao
    {
        private static readonly string _dataBase;
        private static readonly Dictionary<int, Account> _repoAccounts;

        static AccountFileDao()
        {
            _dataBase =
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigurationManager.AppSettings["Accounts"]);

            _repoAccounts = new Dictionary<int, Account>();

            GetData();
        }

        private static void GetData()
        {
            if (!string.IsNullOrEmpty(_dataBase)
                && File.Exists(_dataBase))
            {
                string data = File.ReadAllText(_dataBase);

                var accountsDb = new { Accounts = new List<Account>() };

                accountsDb = JsonConvert.DeserializeAnonymousType(data, accountsDb);

                foreach (Account account in accountsDb.Accounts)
                {
                    _repoAccounts.Add(account.Id, account);
                }
            }
        }

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

        public void Save()
        {
            if (!string.IsNullOrEmpty(_dataBase))
            {
                var accounts = from a in _repoAccounts.Values
                             select new
                             {
                                 a.Id,
                                 a.Login,
                                 a.Password,
                                 a.Role,
                                 a.UserId
                             };

                var db = new { Accounts = accounts };

                string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

                File.WriteAllText(_dataBase, dateBase);
            }
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
