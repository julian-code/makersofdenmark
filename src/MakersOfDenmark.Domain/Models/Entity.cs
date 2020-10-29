using System;
using System.Collections.Generic;
using System.Text;

namespace MakersOfDenmark.Domain.Models
{
    public abstract class Entity<TId> : IEquatable<Entity<TId>>
    {
        public TId Id { get; set; }

        public override bool Equals(object obj)
        {
            var entity = obj as Entity<TId>;
            if (!(entity is null))
            {
                return this.Equals(entity);
            }
            return base.Equals(obj);
        }

        public bool Equals(Entity<TId> other) 
        {
            if (other is null)
            {
                return false;
            }
            return this.Id.Equals(other.Id);
        }   

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static bool operator ==(Entity<TId> obj, Entity<TId> other) 
        {
            return obj.Equals(other);
        }

        public static bool operator !=(Entity<TId> obj, Entity<TId> other) 
        {
            return !(obj.Equals(other));
        }
    }
}