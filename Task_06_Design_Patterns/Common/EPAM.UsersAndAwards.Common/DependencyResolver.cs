using EPAM.UsersAndAwards.BLL;
using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL;
using EPAM.UsersAndAwards.DAL.Interface;

namespace EPAM.UsersAndAwards.Common
{
    public static class DependencyResolver
    {
        #region Type Mode Fields
        
        // Memory fields
        private static IUserDao _userDao;
        private static IAwardDao _awardDao;
        private static IAwardUserDao _awardUserDao;
        private static IAccountDao _accountDao;
        private static IRoleDao _roleDao;

        // File fields
        private static IUserFileDao _userFileDao;
        private static IAwardFileDao _awardFileDao;
        private static IAwardUserFileDao _awardUserFileDao;
        private static IAccountFileDao _accountFileDao;
        private static IRoleFileDao _roleFileDao;
        
        // Database fields
        private static IUserDbDao _userDbDao;
        private static IAwardDbDao _awardDbDao;
        private static IAwardUserDbDao _awardUserDbDao;
        private static IAccountDbDao _accountDbDao;
        private static IRoleDbDao _roleDbDao;

        // Logic fields
        private static IUserLogic _userLogic;
        private static IAwardLogic _awardLogic;
        private static IAwardUserLogic _awardUserLogic;
        private static IAccountLogic _accountLogic;
        private static IRoleLogic _roleLogic;
        
        #endregion

        #region Type Mode

        // Memory mode
        public static IUserDao UserDao => _userDao ?? (_userDao = new UserFakeDao());
        public static IAwardDao AwardDao => _awardDao ?? (_awardDao = new AwardFakeDao());
        public static IAwardUserDao AwardUserDao => _awardUserDao ?? (_awardUserDao = new AwardUserFakeDao());
        public static IAccountDao AccountDao => _accountDao ?? (_accountDao = new AccountFakeDao());
        public static IRoleDao RoleDao => _roleDao ?? (_roleDao = new RoleFakeDao());

        // Database mode
        public static IUserDao UserDbDao => _userDbDao ?? (_userDbDao = new UserDbDao());
        public static IAwardDao AwardDbDao => _awardDbDao ?? (_awardDbDao = new AwardDbDao());
        public static IAwardUserDbDao AwardUserDbDao => _awardUserDbDao ?? (_awardUserDbDao = new AwardUserDbDao());
        public static IAccountDbDao AccountDbDao => _accountDbDao ?? (_accountDbDao = new AccountDbDao());
        public static IRoleDbDao RoleDbDao => _roleDbDao ?? (_roleDbDao = new RoleDbDao());

        // File mode
        public static IUserFileDao UserFileDao => _userFileDao ?? (_userFileDao = new UserFileDao());
        public static IAwardFileDao AwardFileDao => _awardFileDao ?? (_awardFileDao = new AwardFileDao());
        public static IAwardUserFileDao AwardUserFileDao => _awardUserFileDao ?? (_awardUserFileDao = new AwardUserFileDao());
        public static IAccountFileDao AccountFileDao => _accountFileDao ?? (_accountFileDao = new AccountFileDao());
        public static IRoleFileDao RoleFileDao => _roleFileDao ?? (_roleFileDao = new RoleFileDao());

        #endregion

        #region Type Mode Logic

        // Memory mode logic
        public static IUserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(UserDao));
        public static IAwardLogic AwardLogic => _awardLogic ?? (_awardLogic = new AwardLogic(AwardDao));
        public static IAwardUserLogic AwardUserLogic => _awardUserLogic ?? (_awardUserLogic = new AwardUserLogic(AwardUserDao));
        public static IAccountLogic AccountLogic => _accountLogic ?? (_accountLogic = new AccountLogic(AccountDao));
        public static IRoleLogic RoleLogic => _roleLogic ?? (_roleLogic = new RoleLogic(RoleDao));

        // Database mode logic
        public static IUserLogic UserDbLogic => _userLogic ?? (_userLogic = new UserLogic(UserDbDao));
        public static IAwardLogic AwardDbLogic => _awardLogic ?? (_awardLogic = new AwardLogic(AwardDbDao));
        public static IAwardUserLogic AwardUserDbLogic => _awardUserLogic ?? (_awardUserLogic = new AwardUserLogic(AwardUserDbDao));
        public static IAccountLogic AccountDbLogic => _accountLogic ?? (_accountLogic = new AccountLogic(AccountDbDao));
        public static IRoleLogic RoleDbLogic => _roleLogic ?? (_roleLogic = new RoleLogic(RoleDbDao));

        // File mode logic
        public static IUserLogic UserFileLogic => _userLogic ?? (_userLogic = new UserLogic(UserFileDao));
        public static IAwardLogic AwardFileLogic => _awardLogic ?? (_awardLogic = new AwardLogic(AwardFileDao));
        public static IAwardUserLogic AwardUserFileLogic => _awardUserLogic ?? (_awardUserLogic = new AwardUserLogic(AwardUserFileDao));
        public static IAccountLogic AccountFileLogic => _accountLogic ?? (_accountLogic = new AccountLogic(AccountFileDao));
        public static IRoleLogic RoleFileLogic => _roleLogic ?? (_roleLogic = new RoleLogic(RoleFileDao));

        #endregion
    }
}
