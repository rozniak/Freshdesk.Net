/*
 * Freshdesk.Schema.Contact -- Freshdesk Contact
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
    /// Represents a Freshdesk contact.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Contact
    {
        /// <summary>
        /// Gets whether this contact is active.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("active")]
        public bool Active
        {
            get { return _Active; }
            set { if (!ReadOnlyLocked) _Active = value; }
        }
        private bool _Active;

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
        /// Gets the company to which this contact belongs.
        /// 
        /// This property is read-only.
        /// </summary>
        public long CompanyId
        {
            get { return _CompanyId; }
            set { if (!ReadOnlyLocked) _CompanyId = value; }
        }
        private long _CompanyId;

        /// <summary>
        /// Gets or sets the creation date of this contact.
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
        /// Gets or sets the description of this contact.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the email address assigned to this contact.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("email")]
        public string Email
        {
            get { return _Email; }
            set { if (!ReadOnlyLocked) _Email = value; }
        }
        private string _Email;

        /// <summary>
        /// Gets the unique ID of the contact.
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
        /// Gets the time zone of this contact.
        /// </summary>
        [JsonProperty("time_zone")]
        public string TimeZone
        {
            get { return _TimeZone; }
            set { if (!ReadOnlyLocked) _TimeZone = value; }
        }
        private string _TimeZone;

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
        /// Gets or sets the last updated date of this contact.
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
        /// Initializes a new instance of the Contact class.
        /// </summary>
        public Contact()
        {
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Contact class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        public Contact(string json)
        {
            JsonConvert.PopulateObject(json, this);
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Contact class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        public Contact(JObject obj)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            ReadOnlyLocked = true;
        }
    }
}
