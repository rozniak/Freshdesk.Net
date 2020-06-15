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
using System;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk contact's avatar.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class ContactAvatar
    {
        /// <summary>
        /// Gets or sets the URL for the attachment that represents this avatar's
        /// image.
        /// </summary>
        public Uri AttachmentUrl
        {
            get
            {
                if (_AttachmentUrl != null)
                {
                    return new Uri(_AttachmentUrl);
                }

                return null;
            }
        }
        [JsonProperty("attachment_url")]
        private string _AttachmentUrl { get; set; }

        /// <summary>
        /// Gets the creation date of this avatar.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; private set; }

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
        /// Gets or sets the size of this avatar.
        /// </summary>
        [JsonProperty("size")]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the URL for the thumbnail of this avatar.
        /// </summary>
        public Uri ThumbUrl
        {
            get
            {
                if (_ThumbUrl != null)
                {
                    return new Uri(_ThumbUrl);
                }

                return null;
            }
        }
        [JsonProperty("thumb_url")]
        private string _ThumbUrl { get; set; }

        /// <summary>
        /// Gets the last updated date of this avatar.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; private set; }
    }
}
