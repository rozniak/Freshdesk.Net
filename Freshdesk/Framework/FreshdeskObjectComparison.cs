namespace Freshdesk.Framework
{
    /// <summary>
    /// Specifies the rules to be used by <see cref="FreshdeskObjectComparer"/>
    /// methods.
    /// </summary>
    public enum FreshdeskObjectComparison
    {
        /// <summary>
        /// Compare Freshdesk objects such that they must be entirely equal.
        /// </summary>
        Strict,

        /// <summary>
        /// Compare Freshdesk objects using only the ID attribute.
        /// </summary>
        IdOnly
    }
}
