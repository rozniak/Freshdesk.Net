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
using System.Collections.Generic;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk company.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Company : FreshdeskObject
    {
        /// <summary>
        /// Gets the key-value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; private set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public override FreshdeskObjectKind DataType
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
        /// The Freshdesk connection instance that was used to acquire this company.
        /// </summary>
        private FreshdeskService FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Company class.
        /// </summary>
        public Company() { }

        /// <summary>
        /// Initializes a new instance of the Company class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this company.</param>
        public Company(string json, FreshdeskService fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the Company class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this company.</param>
        public Company(JObject obj, FreshdeskService fdConn = null)
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
            return Name;
        }
    }
}
