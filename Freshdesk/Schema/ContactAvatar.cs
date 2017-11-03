/*
 * Freshdesk.Schema.ContactAvatar -- Freshdesk Contact Avatar
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
    /// Represents a Freshdesk contact's avatar.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ContactAvatar
    {
        /// <summary>
        /// Gets or sets the creation date of this avatar.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Gets or sets the MIME content type of this avatar.
        /// </summary>
        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the ID of this avatar.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the name of this avatar.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the raw source URL of this avatar.
        /// </summary>
        [JsonProperty("avatar_url")]
        public string RawUrl { get; set; }

        /// <summary>
        /// Gets or sets the size of this avatar.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the last updated date of this avatar.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        /// Gets the source URL of this avatar.
        /// </summary>
        public Uri Url { get { return new Uri(this.RawUrl); } }
    }
}
