using Freshdesk.Schema;
using System.Collections.Generic;

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for comparing <see cref="IFreshdeskObject"/> instances.
    /// </summary>
    public sealed class FreshdeskObjectComparer : IEqualityComparer<IFreshdeskObject>
    {
        /// <summary>
        /// Determines whether the specified objects are equal.
        /// </summary>
        /// <param name="x">
        /// The first object to compare.
        /// </param>
        /// <param name="y">
        /// The second object to compare.
        /// </param>
        /// <returns>
        /// True if the specified objects are equal; otherwise, false.
        /// </returns>
        public bool Equals(
            IFreshdeskObject x,
            IFreshdeskObject y
        )
        {
            if (object.ReferenceEquals(x, y))
            {
                return true;
            }

            if (
                object.ReferenceEquals(x, null) ||
                object.ReferenceEquals(y, null)
            )
            {
                return false;
            }

            return x.Id        == y.Id &&
                   x.UpdatedAt == y.UpdatedAt;
        }

        /// <summary>
        /// Returns a hash code for the specified object.
        /// </summary>
        /// <param name="obj">
        /// The object for which to get a hash code.
        /// </param>
        /// <returns>
        /// A hash code for the specified object.
        /// </returns>
        public int GetHashCode(
            IFreshdeskObject obj
        )
        {
            if (object.ReferenceEquals(obj, null))
            {
                return 0;
            }

            return obj.Id.GetHashCode() ^ obj.UpdatedAt.GetHashCode();
        }
    }
}
