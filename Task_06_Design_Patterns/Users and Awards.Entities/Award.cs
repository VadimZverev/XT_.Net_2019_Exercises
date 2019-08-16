using System;
using System.Collections.Generic;

namespace Users_and_Awards.Entities
{
    public class Award
    {
        public Award()
        {
            Id = Guid.NewGuid();
            Users = new List<User>();
        }

        public Guid Id { get; set; }

        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
