﻿namespace EPAM.Social_Network.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public int ProfileId { get; set; }
        public int? RoleId { get; set; }
    }
}
