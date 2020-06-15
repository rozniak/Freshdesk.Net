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

namespace Freshdesk.Framework
{
    /// <summary>
    /// Provides methods for various operations when dealing with Freshdesk items.
    /// </summary>
    public static class FreshdeskUtility
    {
        /// <summary>
        /// Returns a string that represents the Freshdesk data type.
        /// </summary>
        /// <param name="dataType">
        /// The Freshdesk data type.
        /// </param>
        /// <returns>
        /// A string that represents the Freshdesk data type.
        /// </returns>
        public static string TypeToString(
            FreshdeskObjectKind dataType
        )
        {
            switch (dataType)
            {
                case FreshdeskObjectKind.Agent:
                    return "agents";

                case FreshdeskObjectKind.Company:
                    return "companies";

                case FreshdeskObjectKind.Contact:
                    return "contacts";

                case FreshdeskObjectKind.Conversation:
                    return "conversations";

                case FreshdeskObjectKind.Solution:
                    return "solutions";

                case FreshdeskObjectKind.Ticket:
                    return "tickets";

                case FreshdeskObjectKind.TimeEntry:
                    return "time_entries";

                default:
                    throw new NotImplementedException(
                        "Unknown data type specified."
                    );
            }
        }
    }
}
