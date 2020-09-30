using MakersOfDenmark.Domain.Models;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IRepository<MakerSpace, MakerSpaceId> MakerSpaces { get; }
        Task SaveChanges();
    }
}