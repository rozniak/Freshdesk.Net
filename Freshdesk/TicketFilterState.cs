namespace Freshdesk
{
    /// <summary>
    /// Specifies which tickets should be returned by Freshdesk based on their state.
    /// </summary>
    public enum TicketFilterState
    {
        /// <summary>
        /// Filter tickets to 'New and My Open Tickets' for the authenticated agent.
        /// </summary>
        NewAndMyOpen,

        /// <summary>
        /// Filter tickets to ones being watched by the authenticated agent.
        /// </summary>
        Watching,

        /// <summary>
        /// Filter tickets to those marked as spam.
        /// </summary>
        Spam,

        /// <summary>
        /// Filter tickets to those that have been deleted.
        /// </summary>
        Deleted
    }
}
