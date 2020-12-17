using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public class User : Entity<Guid>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string SchoolName { get; set; }
        public ICollection<MakerSpace> MakerSpaces { get; set; } = new List<MakerSpace>();
        public ICollection<Badge> Badges { get; set; } = new List<Badge>();
    }
}
