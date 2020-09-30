using System.Threading.Tasks;
using MakersOfDenmark.Domain.Interfaces.Persistence;

namespace MakersOfDenmark.Infrastructure.Persistence.Repositories
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
        public async Task<TEntity> Read(TId id)
        {
            return await dbSet.FindAsync(id);
        }
        public async Task Create(TEntity entity)
        {
             
        }

        public async Task Delete(TEntity entity)
        {
           
        }
    }
}
