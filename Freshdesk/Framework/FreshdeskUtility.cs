using Freshdesk.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for various operations when dealing with Freshdesk items.
    /// </summary>
    public static class FreshdeskUtility
    {
        /// <summary>
        /// Returns a string that represents the Freshdesk data type.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <returns>
        /// A string that represents the Freshdesk data type.
        /// </returns>
        public static string TypeToString(
            FreshdeskObjectKind dataType
        )
        {
            switch (dataType)
            {
                case FreshdeskObjectKind.Agent:
                    return "agents";

                case FreshdeskObjectKind.Company:
                    return "companies";

                case FreshdeskObjectKind.Contact:
                    return "contacts";

                case FreshdeskObjectKind.Conversation:
                    return "conversations";

                case FreshdeskObjectKind.Solution:
                    return "solutions";

                case FreshdeskObjectKind.Ticket:
                    return "tickets";

                case FreshdeskObjectKind.TimeEntry:
                    return "time_entries";

                default:
                    throw new NotImplementedException(
                        "Unknown data type specified."
                    );
            }
        }
    }
}
