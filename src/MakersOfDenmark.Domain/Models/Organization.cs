using MakersOfDenmark.Domain.Enums;
using System;

namespace MakersOfDenmark.Domain.Models
{
    public class Organization
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public OrganizationType OrganizationType { get; set; }
    }
}
