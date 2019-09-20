using System;
using System.Collections.Generic;

namespace EPAM.UsersAndAwards.Entities
{
    public class Award
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public byte[] Image { get; set; }
    }
}
