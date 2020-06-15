using System.Text;

namespace Freshdesk
{
    /// <summary>
    /// Represents a single query parameter for Freshdesk APIs.
    /// </summary>
    public abstract class FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public abstract string Term { get; }


        /// <summary>
        /// Gets or sets the query value.
        /// </summary>
        public string Value { get; protected set; }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "{0}={1}",
                Term,
                Value
            );
        }


        /// <summary>
        /// Composes a query string from an array of queries.
        /// </summary>
        /// <param name="queries">
        /// The queries.
        /// </param>
        /// <returns>
        /// A query string formed from the provided array of queries.
        /// </returns>
        public static string ComposeAll(
            params FreshdeskQuery[] queries
        )
        {
            int lastQuery = queries.Length - 1;
            var sb        = new StringBuilder();

            for (int i = 0; i < queries.Length; i++)
            {
                FreshdeskQuery query = queries[i];

                sb.Append(query.ToString());

                if (i != lastQuery)
                {
                    sb.Append("&");
                }
            }

            return sb.ToString();
        }
    }
}
