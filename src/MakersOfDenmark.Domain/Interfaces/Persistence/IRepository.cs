using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IRepository <TEntity, TId>
    {
        Task Create(TEntity entity);
        Task Read(TId id);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
    }
}