using System;

namespace EPAM.Social_Network.Entities
{
    public class Profile
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Male { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age
        {
            get
            {
                if (DateOfBirth == null)
                {
                    return null;
                }

                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Value.Year;

                if (DateOfBirth <= today.AddYears(-age))
                    return age;

                return --age;
            }
        }

        public byte[] ProfilePhoto { get; set; }
        public string City { get; set; }
    }
}
