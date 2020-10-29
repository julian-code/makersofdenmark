using MakersOfDenmark.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MakersOfDenmark.Domain.Models.User
{
    public class MODUser : Entity<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<MODRole> Roles { get; set; } = new List<MODRole>();
        public ICollection<MODUserFollowMakerSpace> Follows { get; set; } = new List<MODUserFollowMakerSpace>();
        public DateTimeOffset CreatedAt { get; set; }
        public static MODUser CreateUser(string firstName, string lastName, string email)
        {
            return new MODUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                CreatedAt = DateTimeOffset.UtcNow
            };
        }
        public void AddMakerSpaceRole(MakerSpace makerSpace, MakerSpaceRoles role)
        {
            Roles.Add(new MODRole { User = this, MakerSpace = makerSpace, Role = role });
        }
        public void RemoveMakerSpaceRole(MakerSpace makerSpace, MakerSpaceRoles role)
        {
            var hasRole = Roles.FirstOrDefault(x => x.User == this && x.MakerSpace == makerSpace && x.Role == role);
            if (!(hasRole is null))
            {
                Roles.Remove(hasRole);
            }
        }
    }
}
