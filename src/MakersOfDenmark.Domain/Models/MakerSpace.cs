using MakersOfDenmark.Domain.Enums;
using MakersOfDenmark.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public ICollection<MODUserFollowMakerSpace> Followers { get; set; } = new List<MODUserFollowMakerSpace>();
        public void FollowMakerSpace(MODUser newFollower)
        {
            Followers.Add(new MODUserFollowMakerSpace { User = newFollower, MakerSpace = this, CreatedAt = DateTimeOffset.UtcNow });
        }
        public void UnfollowMakerSpace(MODUser exFollower)
        {
            var isFollowed = Followers.FirstOrDefault(x => x.User == exFollower && x.MakerSpace == this);
            Followers.Remove(isFollowed);
        }
    }
}
