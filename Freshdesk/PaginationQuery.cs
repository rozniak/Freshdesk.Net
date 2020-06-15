using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a pagination query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class PaginationQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "page"; }
        }


        /// <summary>
        /// Initializes a new instance of the PaginationQuery class using the specified
        /// parameter.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        public PaginationQuery(
            int page
        )
        {
            if (page < 1)
            {
                throw new ArgumentException(
                    "Invalid pagination query."
                );
            }

            Value = page.ToString();
        }
    }
}
