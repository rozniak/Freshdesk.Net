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
        /// Gets the agent to which this time entry belongs.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("agent_id")]
        public bool AgentId
        {
            get { return _AgentId; }
            set { if (!ReadOnlyLocked) _AgentId = value; }
        }
        private bool _AgentId;

        /// <summary>
        /// Gets or sets whether this time entry should be marked as billable.
        /// </summary>
        [JsonProperty("billable")]
        public bool Billable { get; set; }

        /// <summary>
        /// Gets the company to which this time entry belongs.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public long CompanyId
        {
            get { return _CompanyId; }
            set { if (!ReadOnlyLocked) _CompanyId = value; }
        }
        private long _CompanyId;
        
        /// <summary>
        /// Gets or sets the creation date of this time entry.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { if (!ReadOnlyLocked) _CreatedAt = value; }
        }
        private DateTime _CreatedAt;

        /// <summary>
        /// Gets or sets the addition/creation date of this time entry.
        /// </summary>
        [JsonProperty("executed_at")]
        public DateTime ExecutedAt
        {
            get { return _ExecutedAt; }
            set { if (!ReadOnlyLocked) _ExecutedAt = value; }
        }
        private DateTime _ExecutedAt;

        /// <summary>
        /// Gets or sets the note attached to this time entry.
        /// </summary>
        [JsonProperty("note")]
        public string Note { get; set; }

        /// <summary>
        /// Gets the unique ID of the time entry.
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
        /// Gets or sets the start date of this time entry.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { if (!ReadOnlyLocked) _StartTime = value; }
        }
        private DateTime _StartTime;

        /// <summary>
        /// Gets the ticket to which this time entry belongs.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("ticket_id")]
        public bool TicketId
        {
            get { return _TicketId; }
            set { if (!ReadOnlyLocked) _TicketId = value; }
        }
        private bool _TicketId;

        /// <summary>
        /// Gets the value indicating whether this time entry is an actively running timer.
        /// </summary>
        [JsonProperty("timer_running")]
        public bool TimerRunning
        {
            get { return _TimerRunning; }
            set { if (!ReadOnlyLocked) _TimerRunning = value; }
        }
        private bool _TimerRunning;

        /// <summary>
        /// Gets or sets the amount of time spent in this time entry.
        /// </summary>
        [JsonProperty("time_spent")]
        public TimeSpan TimeSpent { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of this time entry.
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
        /// Initializes a new instance of the TicketTimeEntry class.
        /// </summary>
        public TicketTimeEntry()
        {
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        public TicketTimeEntry(string json)
        {
            JsonConvert.PopulateObject(json, this);
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the TicketTimeEntry class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        public TicketTimeEntry(JObject obj)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            ReadOnlyLocked = true;
        }
    }
}
