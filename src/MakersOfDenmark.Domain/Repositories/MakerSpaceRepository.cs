using MakersOfDenmark.Domain.Interfaces.Persistence;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Repositories
{
    class MakerSpaceRepository : IMakerSpaceRepository
    {
        public MakerSpace Add(MakerSpace makerSpace)
        {
            throw new NotImplementedException();
        }

        public Task Create(MakerSpaceEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(MakerSpaceEntity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MakerSpace> GetAll()
        {
            throw new NotImplementedException();
        }

        public MakerSpace GetMakerSpaceById(MakerSpaceId makerSpaceId)
        {
            throw new NotImplementedException();
        }

        public Task Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public MakerSpace Remove(MakerSpaceId makerSpaceId)
        {
            throw new NotImplementedException();
        }

        public Task Update(MakerSpaceEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
