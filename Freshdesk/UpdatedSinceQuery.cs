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

using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a date updated filter query parameter for Freshdesk API.
    /// </summary>
    public sealed class UpdatedSinceQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "updated_since"; }
        }


        /// <summary>
        /// Initializes a new instance of the UpdatedSinceQuery class using the
        /// specified parameter.
        /// </summary>
        /// <param name="threshold">
        /// The date threshold.
        /// </param>
        public UpdatedSinceQuery(
            DateTime threshold
        )
        {
            Value = threshold.ToString("o");
        }
    }
}
