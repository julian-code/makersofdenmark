using MakersOfDenmark.Domain.Interfaces.Persistence;
using MakersOfDenmark.Domain.Models;
using MakersOfDenmark.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Unit_of_Work
{
    class UnitOfWork : IUnitOfWork
    {
        protected DbContext dbContext;
        protected Repository<MakerSpace, MakerSpaceId> _makerSpaces;

        public IRepository<MakerSpace, MakerSpaceId> makerSpaces
        {
            get
            {
                
                //Den her linje er vist noget fusk:
                return _makerSpaces ?? (_makerSpaces = new Repository<MakerSpace, MakerSpaceId>(dbContext));
            }
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
