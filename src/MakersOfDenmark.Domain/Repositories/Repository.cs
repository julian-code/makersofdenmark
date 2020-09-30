using MakersOfDenmark.Domain.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Repositories
{
    class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }
        public async Task Create(TEntity entity)
        {
            return await Context.Set<TEntity>().Add(entity);
        }

        public async Task Delete(TEntity entity)
        {
            return await Context.Set<TEntity>().Remove(entity);
        }

        public async Task Read(TId id)
        {
            return await Context.Set<TEntity>().Find(id);
        }
    }
}
