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
    public sealed class Company : IFreshdeskObject
    {
        /// <summary>
        /// Gets the company's creation timestamp.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

        /// <summary>
        /// Gets the key-value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; private set; }

        /// <summary>
        /// Gets the data type of the object.
        /// </summary>
        public FreshdeskObjectKind DataType
        {
            get { return FreshdeskObjectKind.Company; }
        }

        /// <summary>
        /// Gets or sets the description of the company.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the domains of the company. Email addresses of contacts that contain this domain will be associated with that company automatically.
        /// </summary>
        [JsonProperty("domains")]
        public List<string> Domains { get; private set; }

        /// <summary>
        /// Gets the unique ID of the company.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; private set; }

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
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; private set; }


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this company.
        /// </summary>
        private FreshdeskConnection FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Company class.
        /// </summary>
        public Company() { }

        /// <summary>
        /// Initializes a new instance of the Company class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this company.</param>
        public Company(string json, FreshdeskConnection fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the Company class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this company.</param>
        public Company(JObject obj, FreshdeskConnection fdConn = null)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            FreshdeskConnection = fdConn;
        }
    }
}
