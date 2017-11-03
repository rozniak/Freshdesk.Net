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
    public sealed class Agent
    {
        /// <summary>
        /// Gets or sets the value that indicates whether this agent is accepting new tickets.
        /// </summary>
        [JsonProperty("available")]
        public bool Available { get; set; }

        /// <summary>
        /// Gets the date/time that this agent became available.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("available_since", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime AvailableSince
        {
            get { return _AvailableSince; }
            set { if (!ReadOnlyLocked) _AvailableSince = value; }
        }
        private DateTime _AvailableSince;

        /// <summary>
        /// Gets or sets the contact information of this agent.
        /// </summary>
        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        /// <summary>
        /// Gets the agent's creation timestamp.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { if (!ReadOnlyLocked) _CreatedAt = value; }
        }
        private DateTime _CreatedAt;

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
        /// Gets the unique ID of the agent.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("id")]
        public long Id
        {
            get { return _Id; }
            set { if (!ReadOnlyLocked) _Id = value; }
        }
        private long _Id;

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
        /// Gets the agent's last updated timestamp.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get { return _UpdatedAt; }
            set { if (!ReadOnlyLocked) _UpdatedAt = value; }
        }
        private DateTime _UpdatedAt;


        /// <summary>
        /// The value indicating whether the read-only properties are locked.
        /// </summary>
        private bool ReadOnlyLocked { get; set; }


        /// <summary>
        /// Initializes a new instance of the Agent class.
        /// </summary>
        public Agent()
        {
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Agent class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        public Agent(string json)
        {
            JsonConvert.PopulateObject(json, this);
            JObject jObj = (JObject)JObject.Parse(json)["contact"];

            Contact = new Contact(jObj);

            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Agent class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        public Agent(JObject obj)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            Contact = new Contact((JObject)obj["contact"]);

            ReadOnlyLocked = true;
        }
    }
}
