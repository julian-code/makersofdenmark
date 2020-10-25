using System;
using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class Tool : Entity<int>
    {
        public ICollection<MakerSpace> MakerSpaces { get; set; }
        public ICollection<Category> Categories { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Name => $"{Model}, {Make}";
    }
}