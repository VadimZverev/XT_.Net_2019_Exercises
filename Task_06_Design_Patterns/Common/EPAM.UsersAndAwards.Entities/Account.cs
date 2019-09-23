namespace EPAM.UsersAndAwards.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int? RoleId { get; set; }
    }
}
