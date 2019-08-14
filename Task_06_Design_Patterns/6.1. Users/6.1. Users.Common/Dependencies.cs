using _61_Users.DAL;
using _61_Users.Entities;

namespace _61_Users.Common
{
    public static class Dependencies
    {
        static Dependencies()
        {
            UsersStorage = new FileStorage();
        }

        public static IStorable UsersStorage { get; }
    }
}
