/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  Rory Fewell (rory.fewell@agileict.co.uk)
 *  
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
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
        private FreshdeskService FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Agent class.
        /// </summary>
        public Agent() { }

        /// <summary>
        /// Initializes a new instance of the Agent class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this agent.</param>
        public Agent(string json, FreshdeskService fdConn = null)
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
        public Agent(JObject obj, FreshdeskService fdConn = null)
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
