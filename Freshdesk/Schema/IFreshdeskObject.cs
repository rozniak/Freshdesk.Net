using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Defines properties common to all Freshdesk objects.
    /// </summary>
    public interface IFreshdeskObject
    {
        /// <summary>
        /// Gets the date the object was created.
        /// </summary>
        DateTime CreatedAt { get; }

        /// <summary>
        /// Gets the data type of the object.
        /// </summary>
        FreshdeskObjectKind DataType { get; }

        /// <summary>
        /// Gets the unique ID of the object.
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Gets the date the object was updated.
        /// </summary>
        DateTime UpdatedAt { get; }
    }
}
