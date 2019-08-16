using Users_and_Awards.DAL;
using Users_and_Awards.Entities;

namespace Users_and_Awards.Common
{
    public static class Dependencies
    {
        static Dependencies()
        {
            UsersStorage = new FileStorage();

            AwardsStorage = AwardsStorage = UsersStorage as FileStorage;
        }

        public static IAwardStorable AwardsStorage { get; }
        public static IUserStorable UsersStorage { get; }
        public static IAwardUserStorable AwardUsersStorage { get; set; }
    }
}
