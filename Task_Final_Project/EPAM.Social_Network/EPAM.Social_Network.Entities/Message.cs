using System;

namespace EPAM.Social_Network.Entities
{
    public class Message
    {
        public int AccountFromId { get; set; }
        public int AccountToId { get; set; }
        public string Text { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
