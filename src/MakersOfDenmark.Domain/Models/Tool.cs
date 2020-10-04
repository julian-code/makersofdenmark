using System;
using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class Tool
    {
        public int Id { get; set; }
        public ICollection<MakerSpace> MakerSpaces { get; set; }
        public ICollection<Category> Categories { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}