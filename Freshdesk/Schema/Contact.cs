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
    /// Represents a Freshdesk contact.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Contact : FreshdeskObject
    {
        /// <summary>
        /// Gets whether this contact is active.
        /// </summary>
        [JsonProperty("active")]
        public bool Active { get; private set; }

        /// <summary>
        /// Gets or sets the address of this contact.
        /// </summary>
        [JsonProperty("address")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the avatar used by this contact.
        /// </summary>
        [JsonProperty("avatar")]
        public ContactAvatar Avatar { get; set; }

        /// <summary>
        /// Gets or sets whether this contact can view all tickets made by the company they belong in.
        /// </summary>
        [JsonProperty("view_all_tickets")]
        public bool CanViewAllTickets { get; set; }

        /// <summary>
        /// Gets or sets the company to which this contact belongs.
        /// </summary>
        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public long CompanyId { get; set; }

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
            get { return FreshdeskObjectKind.Contact; }
        }

        /// <summary>
        /// Gets or sets the description of this contact.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the email address assigned to this contact.
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Determines whether this contact has been deleted.
        /// </summary>
        /// <remarks>
        /// Deleted contacts will not be displayed unless "state=deleted" is passed in the API query string.
        /// 
        /// This field is also not returned by Freshdesk at all for contacts that aren't deleted.
        /// </remarks>
        [JsonProperty("deleted", NullValueHandling = NullValueHandling.Ignore)]
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Gets or sets the job title of this contact.
        /// </summary>
        [JsonProperty("job_title")]
        public string JobTitle { get; set; }

        /// <summary>
        /// Gets or sets the language of this contact.
        /// </summary>
        [JsonProperty("language")]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the mobile phone number of this contact.
        /// </summary>
        [JsonProperty("mobile")]
        public string MobilePhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the name of this contact.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the phone number of this contact.
        /// </summary>
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the time zone of this contact.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone { get; set; }

        /// <summary>
        /// Gets or sets twitter ID of this contact.
        /// </summary>
        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Gets or sets the tags applied to this contact.
        /// </summary>
        public string[] Tags { get; set; }

        /// <summary>
        /// Gets or sets the other emails assigned to this contact.
        /// </summary>
        public string[] OtherEmails { get; set; }


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this contact.
        /// </summary>
        private FreshdeskService FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Contact class.
        /// </summary>
        public Contact() { }

        /// <summary>
        /// Initializes a new instance of the Contact class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this contact.</param>
        public Contact(string json, FreshdeskService fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the Contact class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this contact.</param>
        public Contact(JObject obj, FreshdeskService fdConn = null)
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
