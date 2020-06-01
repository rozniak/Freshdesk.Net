using Freshdesk.Schema;
using System;
using System.Collections.Generic;

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for comparing <see cref="IFreshdeskObject"/> instances.
    /// </summary>
    public sealed class FreshdeskObjectComparer : IEqualityComparer<IFreshdeskObject>
    {
        /// <summary>
        /// Gets or sets the enumeration value that specifies how the objects will be
        /// compared.
        /// </summary>
        public FreshdeskObjectComparison ComparisonType { get; private set; }


        /// <summary>
        /// Initializes a new instance of the FreshdeskObjectComparer class using the
        /// specified comparison type.
        /// </summary>
        /// <param name="comparisonType">
        /// One of the enumeration values that specifies how the objects will be
        /// compared.
        /// </param>
        public FreshdeskObjectComparer(
            FreshdeskObjectComparison comparisonType
        )
        {
            ComparisonType = comparisonType;
        }


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

            switch (ComparisonType)
            {
                case FreshdeskObjectComparison.IdOnly:
                    return x.Id == y.Id;

                case FreshdeskObjectComparison.Strict:
                    return x.Id        == y.Id &&
                           x.UpdatedAt == y.UpdatedAt;

                default:
                    throw new ArgumentException(
                        "No handler for the given comparison type."
                    );
            }
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

            switch (ComparisonType)
            {
                case FreshdeskObjectComparison.IdOnly:
                    return obj.Id.GetHashCode();

                case FreshdeskObjectComparison.Strict:
                    return obj.Id.GetHashCode() ^ obj.UpdatedAt.GetHashCode();

                default:
                    throw new ArgumentException(
                        "No handler for the given comparison type."
                    );
            }
        }
    }
}
