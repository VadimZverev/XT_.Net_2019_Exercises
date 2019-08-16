using Users_and_Awards.Common;
using Users_and_Awards.Entities;
using System;
using System.Collections.Generic;

namespace Users_and_Awards.BLL
{
    public static class UsersManager
    {
        public static IAwardStorable AwardsStorage => Dependencies.AwardsStorage;
        public static IUserStorable UsersStorage => Dependencies.UsersStorage;
        public static IAwardUserStorable AwardUserStorage => Dependencies.AwardUsersStorage;

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
            return AwardsStorage.RemoveAward(title);
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
                user.Awards.Remove(award);
                award.Users.Remove(user);

                return true;
            }
            else
                return false;
        }

        public static void Save()
        {
            AwardUserStorage.Save();
        }
    }
}
