using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a date updated filter query parameter for Freshdesk API.
    /// </summary>
    public sealed class UpdatedSinceQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "updated_since"; }
        }


        /// <summary>
        /// Initializes a new instance of the UpdatedSinceQuery class using the
        /// specified parameter.
        /// </summary>
        /// <param name="threshold">
        /// The date threshold.
        /// </param>
        public UpdatedSinceQuery(
            DateTime threshold
        )
        {
            Value = threshold.ToString("o");
        }
    }
}
