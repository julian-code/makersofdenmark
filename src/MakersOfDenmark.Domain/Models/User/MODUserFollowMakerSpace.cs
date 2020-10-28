using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models.User
{
    public class MODUserFollowMakerSpace : Entity<int>
    {
        public MODUser User { get; set; }
        public MakerSpace MakerSpace { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
