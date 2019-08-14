using _61_Users.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _61_Users.DAL
{
    public class FileStorage : IStorable
    {
        public FileStorage()
        {
            GetUsers();
        }

        private static List<User> Users { get; set; }

        public bool AddUser(User user)
        {
            if (!Users.Exists(u => u.Id == user.Id))
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        public ICollection<User> GetAllUsers() => Users;

        public User GetUser(string name) =>
            Users.FirstOrDefault(u => u.Name == name);

        public bool RemoveUser(string name)
        {
            User user = Users.FirstOrDefault(u => u.Name == name);

            if (user == null)
                return false;

            return Users.Remove(user);
        }

        public void Save()
        {
            string dateBase = JsonConvert.SerializeObject(new { Users }, Formatting.Indented);

            File.WriteAllText("DataBase.json", dateBase);
        }

        private void GetUsers()
        {
            if (File.Exists("DataBase.json"))
            {
                string users = File.ReadAllText("DataBase.json");
                JObject db = JObject.Parse(users);

                if (db.ContainsKey("Users"))
                    Users = db["Users"].ToObject<List<User>>();
                else
                    Users = new List<User>();
            }
            else
                Users = new List<User>();
        }
    }
}
