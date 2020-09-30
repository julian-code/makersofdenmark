using MakersOfDenmark.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Repositories
{
    class Repository<TEntity, TId> : IRepository<TEntity, TId>
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public Task Create(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task Read(TId id)
        {
            throw new NotImplementedException();
        }

        public Task Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
