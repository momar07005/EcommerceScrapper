using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public abstract class Entity<T> where T : Entity<T>
    {
        public int Id { get; set; }

        public override bool Equals(object objectToComparedWith)
        {
            var entityToCompareWith = objectToComparedWith as T;

            if (ReferenceEquals(entityToCompareWith, null))
                return false;

            if (ReferenceEquals(this, entityToCompareWith))
                return true;

            return EqualsCore(entityToCompareWith);
        }

        public static bool operator ==(Entity<T> a, Entity<T> b) => a.Equals(b);

        public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);

        public override int GetHashCode() => GetHashCodeCore();

        protected abstract bool EqualsCore(T obj);

        protected abstract int GetHashCodeCore();
    }
}
