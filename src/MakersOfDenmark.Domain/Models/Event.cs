using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public class Event : Entity<Guid>
    {
        public string Address { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public string Description { get; set; }
        public ICollection<User> Participants { get; set; } = new List<User>();
        public string Badge { get; set; }
    }
}
