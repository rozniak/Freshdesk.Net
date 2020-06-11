/*
 * Freshdesk.Schema.Agent -- Freshdesk Agent
 *
 * This source-code is part of the Freshdesk API for C# library by Rory Fewell (rozniak) of Oddmatics for Agile ICT for Education Ltd.:
 * <<https://oddmatics.uk>>
 * <<http://www.agileict.co.uk>>
 * 	
 * Copyright (C) 2017 Oddmatics
 * 	
 * Sharing, editing and general licence term information can be found inside of the "LICENSE.MD" file that should be located in the root of this project's directory structure.
 */

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk agent
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Agent : FreshdeskObject
    {
        /// <summary>
        /// Gets or sets the value that indicates whether this agent is accepting new tickets.
        /// </summary>
        [JsonProperty("available")]
        public bool Available { get; set; }

        /// <summary>
        /// Gets the date/time that this agent became available.
        /// </summary>
        [JsonProperty("available_since", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AvailableSince { get; private set; }

        /// <summary>
        /// Gets or sets the contact information of this agent.
        /// </summary>
        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public override FreshdeskObjectKind DataType
        {
            get { return FreshdeskObjectKind.Agent; }
        }

        /// <summary>
        /// Gets or sets the group IDs associated with this agent.
        /// </summary>
        [JsonProperty("group_ids")]
        public long[] GroupIds { get; set; }

        /// <summary>
        /// Gets or sets the signature of this agent in HTML format.
        /// </summary>
        [JsonProperty("signature", NullValueHandling = NullValueHandling.Ignore)]
        public string HtmlSignature { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether this agent is only occasionally an agent.
        /// </summary>
        [JsonProperty("occasional")]
        public bool IsOccasional { get; set; }

        /// <summary>
        /// Gets or sets the role IDs associated with this agent.
        /// </summary>
        [JsonProperty("role_ids")]
        public long[] RoleIds { get; set; }

        /// <summary>
        /// Gets or sets the ticket permissions for this agent.
        /// </summary>
        [JsonProperty("ticket_scope")]
        public TicketScope TicketScope { get; set; }


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this agent.
        /// </summary>
        private FreshdeskConnection FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Agent class.
        /// </summary>
        public Agent() { }

        /// <summary>
        /// Initializes a new instance of the Agent class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this agent.</param>
        public Agent(string json, FreshdeskConnection fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);
            JObject jObj = (JObject)JObject.Parse(json)["contact"];

            Contact = new Contact(jObj);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the Agent class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this agent.</param>
        public Agent(JObject obj, FreshdeskConnection fdConn = null)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            Contact = new Contact((JObject)obj["contact"]);

            FreshdeskConnection = fdConn;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Contact.Name;
        }
    }
}
