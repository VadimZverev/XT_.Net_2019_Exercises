using Users_and_Awards.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Users_and_Awards.DAL
{
    public class FileStorage : IUserStorable, IAwardStorable, IAwardUserStorable
    {
        public FileStorage()
        {
            GetData();
        }

        private static List<Award> Awards { get; set; }
        private static List<User> Users { get; set; }
        private static List<AwardUser> AwardUsers { get; set; }

        public bool AddAward(Award award)
        {
            if (!Awards.Exists(a => a.Id == award.Id
                || a.Title == award.Title))
            {
                Awards.Add(award);
                return true;
            }

            return false;
        }

        public bool AddAwardUser(AwardUser awardUser)
        {
            if (!AwardUsers.Exists(
                ua => ua.AwardId == awardUser.AwardId
                      && ua.UserId == awardUser.UserId))
            {
                AwardUsers.Add(awardUser);
                return true;
            }

            return false;
        }

        public bool AddUser(User user)
        {
            if (!Users.Exists(u => u.Id == user.Id))
            {
                Users.Add(user);
                return true;
            }

            return false;
        }

        public ICollection<Award> GetAllAwards() => Awards;

        public ICollection<AwardUser> GetAllAwardUser() => AwardUsers;

        public ICollection<User> GetAllUsers() => Users;

        public Award GetAward(string title) =>
            Awards.FirstOrDefault(a => a.Title == title);

        public AwardUser GetAwardUser(string awardId, string userId) =>
            AwardUsers.FirstOrDefault(
                au => au.AwardId.ToString() == awardId
                      && au.UserId.ToString() == userId);

        public User GetUser(string name) =>
            Users.FirstOrDefault(u => u.Name == name);

        public bool RemoveAward(string title)
        {
            Award award = GetAward(title);

            if (award == null)
                return false;

            return Awards.Remove(award);
        }

        public bool RemoveAwardUser(string awardId, string userId)
        {
            AwardUser awardUser = GetAwardUser(awardId, userId);

            if (awardUser == null)
                return false;

            return AwardUsers.Remove(awardUser);
        }

        public bool RemoveUser(string name)
        {
            User user = GetUser(name);

            if (user == null)
                return false;

            return Users.Remove(user);
        }

        public void Save()
        {
            var awards = from a in Awards
                         select new
                         {
                             a.Id,
                             a.Title
                         };

            var users = from u in Users
                        select new
                        {
                            u.Id,
                            u.Name,
                            u.DateOfBirth,
                            u.Age
                        };

            var awardUser = from au in AwardUsers
                            select new
                            {
                                au.AwardId,
                                au.UserId
                            };

            var db = new { Awards = awards, Users = users, AwardUser = awardUser };

            string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

            File.WriteAllText("DataBase.json", dateBase);
        }

        private void GetData()
        {
            if (File.Exists("DataBase.json"))
            {
                string dateBase = File.ReadAllText("DataBase.json");
                JObject db = JObject.Parse(dateBase);

                if (db.ContainsKey("Awards"))
                    Awards = db["Awards"].ToObject<List<Award>>();
                else
                    Awards = new List<Award>();

                if (db.ContainsKey("Users"))
                    Users = db["Users"].ToObject<List<User>>();
                else
                    Users = new List<User>();

                if (db.ContainsKey("AwardUser"))
                    AwardUsers = db["AwardUser"].ToObject<List<AwardUser>>();
                else
                    AwardUsers = new List<AwardUser>();

                if (AwardUsers.Count != 0)
                {
                    for (int i = 0; i < AwardUsers.Count; i++)
                    {
                        AwardUsers[i].Award = Awards.FirstOrDefault(a => a.Id == AwardUsers[i].AwardId);
                        AwardUsers[i].User = Users.FirstOrDefault(u => u.Id == AwardUsers[i].UserId);
                    }

                    for (int i = 0; i < Users.Count; i++)
                    {
                        var awards = AwardUsers.Where(au => au.UserId == Users[i].Id)
                                                  .Select(au => au.Award)
                                                  .ToList();
                        Users[i].Awards = awards;
                    }

                    for (int i = 0; i < Awards.Count; i++)
                    {
                        var users = AwardUsers.Where(au => au.UserId == Users[i].Id)
                                                  .Select(au => au.User)
                                                  .ToList();
                        Awards[i].Users = users;
                    }
                }
            }
            else
            {
                Awards = new List<Award>();
                Users = new List<User>();
                AwardUsers = new List<AwardUser>();
            }
        }
    }
}
