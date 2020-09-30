using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Interfaces.Persistence
{
    public interface IUnitOfWork
    {
        IMakerSpaceRepository MakerSpaces { get; }
        Task SaveChanges();
    }
}