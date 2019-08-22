using System;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.Entities
{
    public class Award
    {
        public Award()
        {
            Users = new List<User>();
        }

        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
