using Users_and_Awards.Common;
using Users_and_Awards.Entities;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Users_and_Awards.BLL
{
    public static class DataManager
    {
        private static IAwardRepository AwardsStorage => Dependencies.AwardsStorage;
        private static IUserRepository UsersStorage => Dependencies.UsersStorage;
        private static IAwardUserRepository AwardUserStorage => Dependencies.AwardUsersStorage;
        private static ISaved SaveToFile => Dependencies.SaveToFile;

        public static bool AddAwardToUser(string userName, string awardTitle)
        {
            Award award = AwardsStorage.GetAward(awardTitle);
            User user = UsersStorage.GetUser(userName);

            if (award != null && user != null)
            {
                if (AwardUserStorage.AddAwardUser(
                    new AwardUser
                    {
                        AwardId = award.Id,
                        Award = award,
                        UserId = user.Id,
                        User = user
                    }))
                {
                    user.Awards.Add(award);
                    award.Users.Add(user);

                    return true;
                }
            }

            return false;
        }

        public static bool AddAward(string title)
        {
            return AwardsStorage.AddAward(
                         new Award
                         {
                             Title = title
                         });
        }

        public static bool AddUser(string name, DateTime dateOfBirth)
        {
            return UsersStorage.AddUser(
                        new User
                        {
                            Name = name,
                            DateOfBirth = dateOfBirth
                        });
        }

        public static IEnumerable<Award> GetAllAwards()
        {
            return AwardsStorage.GetAllAwards();
        }

        public static IEnumerable<User> GetAllUsers()
        {
            return UsersStorage.GetAllUsers();
        }

        public static bool RemoveAward(string title)
        {
            Award award = AwardsStorage.GetAward(title);

            if (award != null && AwardsStorage.RemoveAward(title))
            {
                var users = UsersStorage.GetAllUsers()
                                        .Where(u => u.Awards.Contains(award));

                foreach (User user in users)
                {
                    user.Awards.Remove(award);
                    AwardUserStorage.RemoveAwardUser(award.Id.ToString(), user.Id.ToString());
                }

                return true;
            }

            return false;
        }

        public static bool RemoveUser(string name)
        {
            return UsersStorage.RemoveUser(name);
        }

        public static bool RemoveAwardToUser(string userName, string awardTitle)
        {
            Award award = AwardsStorage.GetAward(awardTitle);
            User user = UsersStorage.GetUser(userName);

            if (award != null && user != null)
            {
                if (AwardUserStorage.RemoveAwardUser(award.Id.ToString(), user.Id.ToString()))
                {
                    user.Awards.Remove(award);
                    award.Users.Remove(user);

                    return true;
                }
            }

            return false;
        }

        public static void Save() => SaveToFile.Save();
    }
}
