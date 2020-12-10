using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public class MakerSpaceEvent
    {
        public string Title { get; set; }
        public List<MODUser> Registrants { get; set; } = new List<MODUser>();
    }
}
