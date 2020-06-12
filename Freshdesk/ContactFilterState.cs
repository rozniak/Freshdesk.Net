namespace Freshdesk
{
    /// <summary>
    /// Specifies which contacts should be returned by Freshdesk based on their state.
    /// </summary>
    public enum ContactFilterState
    {
        /// <summary>
        /// Filter contacts to those who have been blocked.
        /// </summary>
        Blocked,

        /// <summary>
        /// Filter contacts to those who have been deleted.
        /// </summary>
        Deleted,

        /// <summary>
        /// Filter contacts to those who have not verified their accounts.
        /// </summary>
        Unverified,

        /// <summary>
        /// Filter contacts to those who have verified their accounts.
        /// </summary>
        Verified
    }
}
