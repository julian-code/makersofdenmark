using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IRepository <TEntity, TId> where TEntity : class
    {
        Task Create(TEntity entity);
        Task Read(TId id);
        Task Delete(TEntity entity);
    }
}