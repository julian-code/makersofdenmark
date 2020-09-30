using MakersOfDenmark.Domain.Interfaces.Persistence;
using MakersOfDenmark.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Repositories
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        protected readonly DbContext context;
        protected DbSet<TEntity> dbSet;
        
        public Repository(SampleDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
        public async Task Read(TId id)
        {
            //await dbSet.FirstOrDefaultAsync(MakerSpaceId = id);
            //await dbSet.Find(id);
        }
        public async Task Create(TEntity entity)
        {
             
        }

        public async Task Delete(TEntity entity)
        {
           
        }
    }
}
