using System;

namespace _61_Users.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

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
    }
}
