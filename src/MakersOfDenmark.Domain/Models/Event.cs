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
        //public List<User> Participants { get; set; } - skal kobles sammen med Users, som Jesper arbejder på
        public string Badge { get; set; }
        public MakerSpace MakerSpace { get; set; }
    }
}
