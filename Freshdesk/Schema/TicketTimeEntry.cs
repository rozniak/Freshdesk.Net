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
    /// Represents a Freshdesk time entry.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class TicketTimeEntry : FreshdeskObject
    {
        /// <summary>
        /// Gets or sets the ID of the agent to which this time entry belongs.
        /// </summary>
        [JsonProperty("agent_id")]
        public long AgentId { get; set; }

        /// <summary>
        /// Gets or sets whether this time entry should be marked as billable.
        /// </summary>
        [JsonProperty("billable")]
        public bool Billable { get; set; }

        /// <summary>
        /// Gets the ID of the company to which this time entry belongs.
        /// </summary>
        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public long CompanyId { get; private set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public override FreshdeskObjectKind DataType
        {
            get { return FreshdeskObjectKind.TimeEntry; }
        }

        /// <summary>
        /// Gets or sets the addition/creation date of this time entry.
        /// </summary>
        [JsonProperty("executed_at")]
        public DateTime ExecutedAt { get; set; }

        /// <summary>
        /// Gets or sets the note attached to this time entry.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the start date of this time entry.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Gets the ID of the ticket to which this time entry belongs.
        /// </summary>
        [JsonProperty("ticket_id")]
        public long TicketId { get; private set; }

        /// <summary>
        /// Gets or sets the value indicating whether this time entry is an actively running timer.
        /// </summary>
        [JsonProperty("timer_running")]
        public bool TimerRunning { get; set; }

        /// <summary>
        /// Gets or sets the amount of time spent in this time entry.
        /// </summary>
        public TimeSpan TimeSpent
        {
            get
            {
                string[] components   = _TimeSpent.Split(':');
                int      totalHours   = Convert.ToInt32(components[0]);
                int      totalMinutes = Convert.ToInt32(components[1]);

                int days  = totalHours / 24;
                int hours = totalHours % 24;

                return new TimeSpan(
                    days,
                    hours,
                    totalMinutes,
                    0
                );
            }
        }
        [JsonProperty("time_spent")]
        private string _TimeSpent { get; set; }


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this time entry.
        /// </summary>
        private FreshdeskService FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class.
        /// </summary>
        public TicketTimeEntry() { }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this time entry.</param>
        public TicketTimeEntry(string json, FreshdeskService fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this time entry.</param>
        public TicketTimeEntry(JObject obj, FreshdeskService fdConn = null)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

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
            return TimeSpent.ToString("c");
        }
    }
}
