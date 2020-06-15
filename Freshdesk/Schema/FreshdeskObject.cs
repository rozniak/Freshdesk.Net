using Newtonsoft.Json;
using System;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class FreshdeskObject
    {
        /// <summary>
        /// Gets or sets the date the object was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public abstract FreshdeskObjectKind DataType { get; }

        /// <summary>
        /// Gets or sets  the unique ID of the object.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; protected set; }

        /// <summary>
        /// Gets or sets the Freshdesk connection instance associated with the object.
        /// </summary>
        public FreshdeskService Freshdesk { get; internal set; }

        /// <summary>
        /// Gets or sets  the date the object was updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; protected set; }
    }
}
