using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a ticket filter query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class TicketFilterQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "filter"; }
        }


        /// <summary>
        /// Initializes a new instance of the TicketFilterQuery class using the
        /// specified parameter.
        /// </summary>
        /// <param name="state">
        /// The ticket filter state.
        /// </param>
        public TicketFilterQuery(
            TicketFilterState state
        )
        {
            switch (state)
            {
                case TicketFilterState.NewAndMyOpen:
                    Value = "new_and_my_open";
                    break;

                case TicketFilterState.Watching:
                    Value = "watching";
                    break;

                case TicketFilterState.Spam:
                    Value = "spam";
                    break;

                case TicketFilterState.Deleted:
                    Value = "deleted";
                    break;

                default:
                    throw new NotImplementedException(
                        "Filter specifeid is not implemeted."
                    );
            }
        }
    }
}
