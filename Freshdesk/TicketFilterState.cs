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

namespace Freshdesk
{
    /// <summary>
    /// Specifies which tickets should be returned by Freshdesk based on their state.
    /// </summary>
    public enum TicketFilterState
    {
        /// <summary>
        /// Filter tickets to 'New and My Open Tickets' for the authenticated agent.
        /// </summary>
        NewAndMyOpen,

        /// <summary>
        /// Filter tickets to ones being watched by the authenticated agent.
        /// </summary>
        Watching,

        /// <summary>
        /// Filter tickets to those marked as spam.
        /// </summary>
        Spam,

        /// <summary>
        /// Filter tickets to those that have been deleted.
        /// </summary>
        Deleted
    }
}
