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

            return this.Id == entityToCompareWith.Id;
        }

        public override int GetHashCode() => GetHashCodeCore();

        protected abstract int GetHashCodeCore();
    }
}
