using EPAM.UsersAndAwards.BLL;
using EPAM.UsersAndAwards.BLL.Interface;
using EPAM.UsersAndAwards.DAL;
using EPAM.UsersAndAwards.DAL.Interface;

namespace EPAM.UsersAndAwards.Common
{
    public static class DependencyResolver
    {
        private static IUserDao _userDao;
        private static IAwardDao _awardDao;
        private static IAwardUserDao _awardUserDao;
        private static IAccountDao _accountDao;

        private static IUserFileDao _userFileDao;
        private static IAwardFileDao _awardFileDao;
        private static IAwardUserFileDao _awardUserFileDao;
        private static IAccountFileDao _accountFileDao;

        private static IUserLogic _userLogic;
        private static IAwardLogic _awardLogic;
        private static IAwardUserLogic _awardUserLogic;
        private static IAccountLogic _accountLogic;

        public static IUserDao UserDao => _userDao ?? (_userDao = new UserFakeDao());
        public static IAwardDao AwardDao => _awardDao ?? (_awardDao = new AwardFakeDao());
        public static IAwardUserDao AwardUserDao => _awardUserDao ?? (_awardUserDao = new AwardUserFakeDao());

        public static IUserFileDao UserFileDao => _userFileDao ?? (_userFileDao = new UserFileDao());
        public static IAwardFileDao AwardFileDao => _awardFileDao ?? (_awardFileDao = new AwardFileDao());
        public static IAwardUserFileDao AwardUserFileDao => _awardUserFileDao ?? (_awardUserFileDao = new AwardUserFileDao());
        private static IAccountFileDao AccountFileDao => _accountFileDao ?? (_accountFileDao = new AccountFileDao());

        public static IUserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(UserDao));
        public static IAwardLogic AwardLogic => _awardLogic ?? (_awardLogic = new AwardLogic(AwardDao));
        public static IAwardUserLogic AwardUserLogic => _awardUserLogic ?? (_awardUserLogic = new AwardUserLogic(AwardUserDao));

        public static IUserLogic UserFileLogic => _userLogic ?? (_userLogic = new UserLogic(UserFileDao));
        public static IAwardLogic AwardFileLogic => _awardLogic ?? (_awardLogic = new AwardLogic(AwardFileDao));
        public static IAwardUserLogic AwardUserFileLogic => _awardUserLogic ?? (_awardUserLogic = new AwardUserLogic(AwardUserFileDao));
        public static IAccountLogic AccountFileLogic => _accountLogic ?? (_accountLogic = new AccountLogic(AccountFileDao));
    }
}
