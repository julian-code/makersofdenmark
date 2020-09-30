using System;
using System.Collections.Generic;
using System.ComponentModel;
using MakersOfDenmark.Domain.Models;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IMakerSpaceRepository : IRepository<MakerSpaceEntity, Guid>
    {
        IEnumerable<MakerSpace> ReadAll();
    }
}