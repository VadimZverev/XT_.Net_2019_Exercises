using System;
using System.Collections.Generic;

namespace Users_and_Awards.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Awards = new List<Award>();
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;

                if (DateOfBirth <= today.AddYears(-age))
                    return age;

                return --age;
            }
        }

        public ICollection<Award> Awards { get; set; }
    }
}
