using MakersOfDenmark.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models.User
{
    public class MODRole : Entity<int>
    {
        public MODUser User { get; set; }
        public MakerSpace MakerSpace { get; set; }
        public MakerSpaceRoles  Role { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }

}
