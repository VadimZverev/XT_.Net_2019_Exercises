using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Users_and_Awards.Entities;

namespace Users_and_Awards.DAL
{
    public class DataStorage : ISaved
    {
        public DataStorage()
        {
            GetData();
        }

        public AwardsStorage AwardsStorage { get; private set; }
        public UsersStorage UsersStorage { get; private set; }
        public AwardUsersStorage AwardUsersStorage { get; private set; }

        public void Save()
        {
            var awards = from a in AwardsStorage.GetAllAwards()
                         select new
                         {
                             a.Id,
                             a.Title
                         };

            var users = from u in UsersStorage.GetAllUsers()
                        select new
                        {
                            u.Id,
                            u.Name,
                            u.DateOfBirth,
                            u.Age
                        };

            var awardUser = from au in AwardUsersStorage.GetAllAwardUser()
                            select new
                            {
                                au.AwardId,
                                au.UserId
                            };

            var db = new { Awards = awards, Users = users, AwardUsers = awardUser };

            string dateBase = JsonConvert.SerializeObject(db, Formatting.Indented);

            File.WriteAllText("DataBase.json", dateBase);
        }

        private void GetData()
        {
            if (File.Exists("DataBase.json"))
            {
                var db = new
                {
                    Awards = new List<Award>(),
                    Users = new List<User>(),
                    AwardUsers = new List<AwardUser>()
                };

                string dateBase = File.ReadAllText("DataBase.json");
                db = JsonConvert.DeserializeAnonymousType(dateBase, db);

                if (db.AwardUsers.Count != 0)
                {
                    for (int i = 0; i < db.AwardUsers.Count; i++)
                    {
                        db.AwardUsers[i].Award = 
                            db.Awards.FirstOrDefault(a => a.Id == db.AwardUsers[i].AwardId);

                        db.AwardUsers[i].User = 
                            db.Users.FirstOrDefault(u => u.Id == db.AwardUsers[i].UserId);
                    }

                    for (int i = 0; i < db.Users.Count; i++)
                    {
                        var awards = db.AwardUsers.Where(au => au.UserId == db.Users[i].Id)
                                                  .Select(au => au.Award)
                                                  .ToList();

                        db.Users[i].Awards = awards;
                    }

                    for (int i = 0; i < db.Awards.Count; i++)
                    {
                        var users = db.AwardUsers.Where(au => au.AwardId == db.Awards[i].Id)
                                                  .Select(au => au.User)
                                                  .ToList();

                        db.Awards[i].Users = users;
                    }

                    AwardsStorage = new AwardsStorage(db.Awards);
                    UsersStorage = new UsersStorage(db.Users);
                    AwardUsersStorage = new AwardUsersStorage(db.AwardUsers);
                }
            }
            else
            {
                AwardsStorage = new AwardsStorage();
                UsersStorage = new UsersStorage();
                AwardUsersStorage = new AwardUsersStorage();
            }
        }
    }
}