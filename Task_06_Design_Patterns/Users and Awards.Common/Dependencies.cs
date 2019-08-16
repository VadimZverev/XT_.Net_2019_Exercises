using Users_and_Awards.DAL;
using Users_and_Awards.Entities;

namespace Users_and_Awards.Common
{
    public static class Dependencies
    {
        static Dependencies()
        {
            var data = new DataStorage();

            AwardsStorage = data.AwardsStorage;
            AwardUsersStorage = data.AwardUsersStorage;
            SaveToFile = data;
            UsersStorage = data.UsersStorage;
        }

        public static IAwardRepository AwardsStorage { get; }
        public static IUserRepository UsersStorage { get; }
        public static IAwardUserRepository AwardUsersStorage { get; set; }
        public static ISaved SaveToFile { get; set; }
    }
}
