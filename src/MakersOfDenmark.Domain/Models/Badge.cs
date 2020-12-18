using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public class Badge : Entity<Guid>
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public ICollection<MakerSpace> MakerSpaces { get; set; } = new List<MakerSpace>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
