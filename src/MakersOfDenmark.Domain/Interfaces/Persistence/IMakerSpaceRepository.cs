using System;
using System.Collections.Generic;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Shared;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IMakerSpaceRepository : IRepository<MakerSpaceEntity, Guid>
    {
        IEnumerable<MakerSpace> GetAll();
    }
}