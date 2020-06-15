using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a quantifying query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class QuantityQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "per_page"; }
        }


        /// <summary>
        /// Initializes a new instance of the QuantityQuery class using the specified
        /// parameter.
        /// </summary>
        /// <param name="quantity">
        /// The quantity.
        /// </param>
        public QuantityQuery(
            int quantity
        )
        {
            if (quantity < 1)
            {
                throw new ArgumentException(
                    "Invalid quantifier query."
                );
            }

            Value = quantity.ToString();
        }
    }
}
