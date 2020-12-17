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
    }
}
