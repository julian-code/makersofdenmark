using MakersOfDenmark.Domain.Enums;
using System;

namespace MakersOfDenmark.Domain.Models
{
    public class Organization : Entity<Guid>
    {
        public string Name { get; set; }
        public Address Address { get; set; }
        public OrganizationType OrganizationType { get; set; }
    }
}
