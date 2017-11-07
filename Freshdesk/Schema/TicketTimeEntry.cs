/*
 * Freshdesk.Schema.TicketTimeEntry -- Freshdesk Ticket Time Entry
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
    /// Represents a Freshdesk time entry.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class TicketTimeEntry
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
        /// Gets or sets the creation date of this time entry.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

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
        /// Gets the unique ID of the time entry.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; private set; }

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
        [JsonProperty("time_spent")]
        public TimeSpan TimeSpent { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of this time entry.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; private set; }


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this time entry.
        /// </summary>
        private FreshdeskConnection FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class.
        /// </summary>
        public TicketTimeEntry() { }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this time entry.</param>
        public TicketTimeEntry(string json, FreshdeskConnection fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this time entry.</param>
        public TicketTimeEntry(JObject obj, FreshdeskConnection fdConn = null)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            FreshdeskConnection = fdConn;
        }
    }
}
