﻿using MakersOfDenmark.Domain.Interfaces.Persistence;
using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Infrastructure.Persistence.Repositories
{
    class MakerSpaceRepository : Repository<MakerSpace, MakerSpaceId>, IMakerSpaceRepository 
    {

        public MakerSpaceRepository(DbContext context): base(context)
        {

        }
        public Task Read(MakerSpaceId id)
        {
            
        }

        public Task Create(MakerSpace entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(MakerSpace entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MakerSpace> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
