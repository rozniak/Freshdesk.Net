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
    /// Represents a Freshdesk object.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public abstract class FreshdeskObject
    {
        /// <summary>
        /// Gets or sets the date the object was created.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; protected set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public abstract FreshdeskObjectKind DataType { get; }

        /// <summary>
        /// Gets or sets  the unique ID of the object.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; protected set; }

        /// <summary>
        /// Gets or sets the Freshdesk connection instance associated with the object.
        /// </summary>
        public FreshdeskService Freshdesk { get; internal set; }

        /// <summary>
        /// Gets or sets  the date the object was updated.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; protected set; }
    }
}
