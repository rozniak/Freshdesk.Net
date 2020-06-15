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
    /// Represents a pagination query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class PaginationQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "page"; }
        }


        /// <summary>
        /// Initializes a new instance of the PaginationQuery class using the specified
        /// parameter.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        public PaginationQuery(
            int page
        )
        {
            if (page < 1)
            {
                throw new ArgumentException(
                    "Invalid pagination query."
                );
            }

            Value = page.ToString();
        }
    }
}
