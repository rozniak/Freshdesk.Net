/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  Rory Fewell (rory.fewell@agileict.co.uk)
 *  
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using Freshdesk.Schema;
using System;
using System.Collections.Generic;

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for comparing <see cref="FreshdeskObject"/> instances.
    /// </summary>
    public sealed class FreshdeskObjectComparer : IEqualityComparer<FreshdeskObject>
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
            FreshdeskObject x,
            FreshdeskObject y
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
            FreshdeskObject obj
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
