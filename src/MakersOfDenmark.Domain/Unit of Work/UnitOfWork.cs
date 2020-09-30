using MakersOfDenmark.Domain.Interfaces.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MakersOfDenmark.Domain.Unit_of_Work
{
    class UnitOfWork : IUnitOfWork
    {
        public Task SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
