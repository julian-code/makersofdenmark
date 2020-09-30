using MakersOfDenmark.Domain.Interfaces.Persistence;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Repositories
{
    class MakerSpaceRepository : Repository<MakerSpace, MakerSpaceId>, IMakerSpaceRepository 
    {
        public MakerSpaceRepository(DbContext context): base(context)
        {

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

        public Task Read(Guid id)
        {
            throw new NotImplementedException();
        }
    }

}
