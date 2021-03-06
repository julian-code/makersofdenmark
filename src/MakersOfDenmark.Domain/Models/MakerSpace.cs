using MakersOfDenmark.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MakersOfDenmark.Domain.Models
{
    public class MakerSpace : Entity<Guid>
    {
        public string Name { get; set; }
        public string WorkShopType { get; set; }
        public Address Address { get; set; }
        public string AssociatedSchool { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public AccessType AccessType { get; set; }
        public string Organization { get; set; }
        #nullable enable
        public string? VATNumber { get; set; }
        #nullable disable
        public ContactInfo ContactInfo { get; set; }
        public ICollection<Badge> Badges { get; set; } = new List<Badge>();
        public ICollection<User> Members { get; set; } = new List<User>();
        public ICollection<User> Admins { get; set; } = new List<User>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
        public ICollection<Tool> Tools { get; set; } = new List<Tool>();
        public void AddEvent(Event newEvent) 
        {
            Events.Add(newEvent);
        }
    }
}
