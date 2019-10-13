using EPAM.Social_Network.BLL;
using EPAM.Social_Network.BLL.Interfaces;
using EPAM.Social_Network.DAL;
using EPAM.Social_Network.DAL.Interfaces;
using EPAM.Social_Network.Entities;

namespace EPAM.Social_Network.Dependencies
{
    public static class DependencyResolver
    {
        #region Type Mode Fields

        // Database fields
        private static IDbDao<Account> _accountDbDao;
        private static IDbDao<Profile> _profileDbDao;
        private static IDbDao<Role> _roleDbDao;
        private static IFriendDbDao _friendDbDao;
        private static IMessageDbDao _messageDbDao;

        // Logic fields
        private static IDbLogic<Account> _accountDbLogic;
        private static IDbLogic<Profile> _profileDbLogic;
        private static IDbLogic<Role> _roleDbLogic;
        private static IFriendDbLogic _friendDbLogic;
        private static IMessageDbLogic _messageDbLogic;

        #endregion

        #region Type Mode

        // Database mode
        public static IDbDao<Account> AccountDbDao => _accountDbDao ?? (_accountDbDao = new AccountDao());
        public static IDbDao<Profile> ProfileDbDao => _profileDbDao ?? (_profileDbDao = new ProfileDao());
        public static IDbDao<Role> RoleDbDao => _roleDbDao ?? (_roleDbDao = new RoleDao());
        public static IFriendDbDao FriendDbDao => _friendDbDao ?? (_friendDbDao = new FriendDao());
        public static IMessageDbDao MessageDao => _messageDbDao ?? (_messageDbDao = new MessageDao());

        #endregion

        #region Type Mode Logic

        // Database mode logic
        public static IDbLogic<Account> AccountDbLogic => _accountDbLogic ?? (_accountDbLogic = new AccountLogic(AccountDbDao));
        public static IDbLogic<Profile> ProfileDbLogic => _profileDbLogic ?? (_profileDbLogic = new ProfileLogic(ProfileDbDao));
        public static IDbLogic<Role> RoleDbLogic => _roleDbLogic ?? (_roleDbLogic = new RoleLogic(RoleDbDao));
        public static IFriendDbLogic FriendDbLogic => _friendDbLogic ?? (_friendDbLogic = new FriendLogic(FriendDbDao));
        public static IMessageDbLogic MessageDbLogic => _messageDbLogic ?? (_messageDbLogic = new MessageLogic(MessageDao));

        #endregion
    }
}
