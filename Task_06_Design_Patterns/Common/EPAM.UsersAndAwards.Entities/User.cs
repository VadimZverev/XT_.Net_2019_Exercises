using System;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.Entities
{
    public class User
    {
        public User()
        {
            Awards = new List<Award>();
        }

        public int Id { get; set; }

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
