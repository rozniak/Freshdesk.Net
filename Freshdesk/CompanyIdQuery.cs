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

using Freshdesk.Schema;
using System;

namespace Freshdesk
{
    /// <summary>
    /// Represents a company ID filter query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class CompanyIdQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "company_id"; }
        }


        /// <summary>
        /// Initializes a new instance of the CompanyIdQuery class using the specified
        /// parameter.
        /// </summary>
        /// <param name="company">
        /// The company.
        /// </param>
        public CompanyIdQuery(
            Company company
        )
        {
            Value = company.Id.ToString();
        }

        /// <summary>
        /// Initializes a new instance of the CompanyIdQuery class using the specified
        /// parameter.
        /// </summary>
        /// <param name="id">
        /// The company ID.
        /// </param>
        public CompanyIdQuery(
            long id
        )
        {
            if (id < 1)
            {
                throw new ArgumentException(
                    "Invalid company ID."
                );
            }

            Value = id.ToString();
        }
    }
}
