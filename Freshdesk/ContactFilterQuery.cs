using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a contact filter query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class ContactFilterQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "state"; }
        }


        /// <summary>
        /// Initializes a new instance of the ContactFilterQuery class using the
        /// specified parameter.
        /// </summary>
        /// <param name="state">
        /// The contact filter state.
        /// </param>
        public ContactFilterQuery(
            ContactFilterState state
        )
        {
            switch (state)
            {
                case ContactFilterState.Blocked:
                    Value = "blocked";
                    break;

                case ContactFilterState.Deleted:
                    Value = "deleted";
                    break;

                case ContactFilterState.Unverified:
                    Value = "unverified";
                    break;

                case ContactFilterState.Verified:
                    Value = "verified";
                    break;

                default:
                    throw new NotImplementedException(
                        "Filter specified is not implemented."
                    );
            }
        }
    }
}
