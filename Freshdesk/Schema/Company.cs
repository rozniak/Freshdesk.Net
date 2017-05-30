/*
 * Freshdesk.Schema.Company -- Freshdesk Company
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
using System.Collections.Generic;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk company.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Company
    {
        /// <summary>
        /// Gets the company's creation timestamp.
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
        /// Gets or sets the key-value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Gets or sets the description of the company.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the domains of the company. Email addresses of contacts that contain this domain will be associated with that company automatically.
        /// </summary>
        [JsonProperty("domains")]
        public List<string> Domains { get; set; }

        /// <summary>
        /// Gets the unique ID of the company.
        /// 
        /// This propery is read-only.
        /// </summary>
        [JsonProperty("id")]
        public long Id
        {
            get { return _Id; }
            set { if (!ReadOnlyLocked) _Id = value; }
        }
        private long _Id;

        /// <summary>
        /// Gets or sets the name of the company.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the note about the company.
        /// </summary>
        [JsonProperty("note", NullValueHandling = NullValueHandling.Ignore)]
        public string Note { get; set; }

        /// <summary>
        /// Gets the company's last updated timestamp.
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
        /// Initializes a new instance of the Company class.
        /// </summary>
        public Company()
        {
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Company class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        public Company(string json)
        {
            JsonConvert.PopulateObject(json, this);
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Company class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        public Company(JObject obj)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            ReadOnlyLocked = true;
        }
    }
}
