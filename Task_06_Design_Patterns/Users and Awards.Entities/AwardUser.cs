using System;

namespace Users_and_Awards.Entities
{
    public class AwardUser
    {
        public Guid AwardId { get; set; }
        public Award Award { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
