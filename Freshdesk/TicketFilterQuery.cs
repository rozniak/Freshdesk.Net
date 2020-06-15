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
    /// Represents a ticket filter query parameter for Freshdesk APIs.
    /// </summary>
    public sealed class TicketFilterQuery : FreshdeskQuery
    {
        /// <summary>
        /// Gets or sets the query term.
        /// </summary>
        public override string Term
        {
            get { return "filter"; }
        }


        /// <summary>
        /// Initializes a new instance of the TicketFilterQuery class using the
        /// specified parameter.
        /// </summary>
        /// <param name="state">
        /// The ticket filter state.
        /// </param>
        public TicketFilterQuery(
            TicketFilterState state
        )
        {
            switch (state)
            {
                case TicketFilterState.NewAndMyOpen:
                    Value = "new_and_my_open";
                    break;

                case TicketFilterState.Watching:
                    Value = "watching";
                    break;

                case TicketFilterState.Spam:
                    Value = "spam";
                    break;

                case TicketFilterState.Deleted:
                    Value = "deleted";
                    break;

                default:
                    throw new NotImplementedException(
                        "Filter specifeid is not implemeted."
                    );
            }
        }
    }
}
