using System;

namespace _23_User
{
    class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                if ((today.Month < DateOfBirth.Month) ||
                        (today.Month == DateOfBirth.Month &&
                            today.Day < DateOfBirth.Day))
                {
                    return DateTime.Today.Year - DateOfBirth.Year - 1;
                }
                else
                    return DateTime.Today.Year - DateOfBirth.Year;
            }
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName} {MiddleName}";
        }
    }
}
