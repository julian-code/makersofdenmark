using MakersOfDenmark.Domain.Enums;
using System;
using System.Collections.Generic;

namespace MakersOfDenmark.Domain.Models
{
    public class MakerSpace : Entity<Guid>
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public ContactInfo ContactInfo { get; set; }
        #nullable enable
        public string? VATNumber  { get; set; }
        #nullable disable
        public Uri Logo { get; set; }
        public AccessType AccessType { get; set; }
        public Organization Organization { get; set; }
        public ICollection<Tool> Tools { get; set; } = new List<Tool>();

    }
}
