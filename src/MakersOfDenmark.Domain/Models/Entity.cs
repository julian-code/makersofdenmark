using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public abstract class Entity<TId> 
    {
        public TId Id { get; set; }
    }
}
