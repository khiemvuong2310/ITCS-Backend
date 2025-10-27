using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Core.Models.Bases
{
    /// <summary>
    /// Generic base class for value objects in domain-driven design.
    /// Value objects are immutable and equality is determined by comparing all properties.
    /// </summary>
    /// <typeparam name="T">The type of the value object (must be the derived type itself)</typeparam>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        /// <summary>
        /// When overridden in a derived class, gets all components of the value object that should contribute to equality/inequality.
        /// </summary>
        /// <returns>An enumeration of all equality components.</returns>
        protected abstract IEnumerable<object> GetEqualityComponents();

        /// <summary>
        /// Determines whether the specified object is equal to the current value object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            var other = (ValueObject<T>)obj;

            return GetEqualityComponents()
                .SequenceEqual(other.GetEqualityComponents());
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current value object.</returns>
        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x?.GetHashCode() ?? 0)
                .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Equality operator.
        /// </summary>
        /// <param name="left">The first value object to compare.</param>
        /// <param name="right">The second value object to compare.</param>
        /// <returns>true if the value objects are equal; otherwise, false.</returns>
        public static bool operator ==(ValueObject<T>? left, ValueObject<T>? right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Inequality operator.
        /// </summary>
        /// <param name="left">The first value object to compare.</param>
        /// <param name="right">The second value object to compare.</param>
        /// <returns>true if the value objects are not equal; otherwise, false.</returns>
        public static bool operator !=(ValueObject<T>? left, ValueObject<T>? right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Creates a shallow copy of the current value object.
        /// </summary>
        /// <returns>A shallow copy of the current value object.</returns>
        public T GetCopy()
        {
            return (T)MemberwiseClone();
        }
    }
}
