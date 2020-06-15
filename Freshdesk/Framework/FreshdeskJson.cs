using Freshdesk.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for working with Freshdesk objects in JSON representation.
    /// </summary>
    public static class FreshdeskJson
    {
        /// <summary>
        /// Deserializes JSON into a collection of the specified Freshdesk data type.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="jsonSrc">
        /// The JSON document source.
        /// </param>
        /// <returns>
        /// An <see cref="IEnumerable{FreshdeskObject}"/> collection containing
        /// instances of the Freshdesk data type based on the JSON provided.
        /// </returns>
        public static IEnumerable<FreshdeskObject> DeserializeToCollection(
            FreshdeskObjectKind dataType,
            string              jsonSrc
        )
        {
            switch (dataType)
            {
                case FreshdeskObjectKind.Agent:
                    return JsonConvert
                               .DeserializeObject<List<Agent>>(jsonSrc);

                case FreshdeskObjectKind.Company:
                    return JsonConvert
                               .DeserializeObject<List<Company>>(jsonSrc);

                case FreshdeskObjectKind.Contact:
                    return JsonConvert
                               .DeserializeObject<List<Contact>>(jsonSrc);

                case FreshdeskObjectKind.Conversation:
                    return JsonConvert
                               .DeserializeObject<List<Conversation>>(jsonSrc);

                case FreshdeskObjectKind.Ticket:
                    return JsonConvert
                               .DeserializeObject<List<Ticket>>(jsonSrc);

                case FreshdeskObjectKind.TimeEntry:
                    return JsonConvert
                               .DeserializeObject<List<TicketTimeEntry>>(jsonSrc);

                default:
                    throw new ArgumentException(
                        "Not able to deserialize the specified type."
                    );
            }
        }

        /// <summary>
        /// Deserializes JSON into the specified Freshdesk data type.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <param name="jsonSrc">
        /// The JSON document source.
        /// </param>
        /// <returns>
        /// An instance of the Freshdesk data type based on the JSON provided, casted
        /// as an <see cref="FreshdeskObject"/>.
        /// </returns>
        public static FreshdeskObject DeserializeToType(
            FreshdeskObjectKind dataType,
            string              jsonSrc
        )
        {
            switch (dataType)
            {
                case FreshdeskObjectKind.Agent:
                    return JsonConvert.DeserializeObject<Agent>(jsonSrc);

                case FreshdeskObjectKind.Company:
                    return JsonConvert.DeserializeObject<Company>(jsonSrc);

                case FreshdeskObjectKind.Contact:
                    return JsonConvert.DeserializeObject<Contact>(jsonSrc);

                case FreshdeskObjectKind.Conversation:
                    return JsonConvert.DeserializeObject<Conversation>(jsonSrc);

                case FreshdeskObjectKind.Ticket:
                    return JsonConvert.DeserializeObject<Ticket>(jsonSrc);

                case FreshdeskObjectKind.TimeEntry:
                    return JsonConvert.DeserializeObject<TicketTimeEntry>(jsonSrc);

                default:
                    throw new ArgumentException(
                        "Not able to deserialize the specified type."
                    );
            }
        }
    }
}
