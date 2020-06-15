/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  John Becker (john@beckersoft.com)
 *  Oleg Shevchenko (shevchenko.oleg@outlook.com)
 *  Joseph Poh (github user jozsurf)
 *  (github user ninjacarr)
 *  (github user sloppypointless)
 *  Rory Fewell (rory.fewell@agileict.co.uk)
 *  
 *  Some web code is derived from work authored by:
 * 	Gonzalo Paniagua Javier (gonzalo@xamarin.com)
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

using Freshdesk.Internal;
using Freshdesk.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freshdesk
{
    /// <summary>
    /// Provides an interface to the public Freshdesk API.
    /// </summary>
    public class FreshdeskService
    {
        /// <summary>
        /// Gets or sets the URI of the API endpoint.
        /// </summary>
        public Uri ApiEndpoint
        {
            get { return Endpoint.BaseUri; }
        }

        /// <summary>
        /// Gets or sets the timeout for web requests made to the Freshdesk API.
        /// </summary>
        public int RequestTimeout
        {
            get { return Endpoint.RequestTimeout; }
            set { Endpoint.RequestTimeout = value; }
        }


        /// <summary>
        /// The communication layer for performing the API calls to Freshdesk.
        /// </summary>
        private FreshdeskEndpoint Endpoint { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FreshdeskService"/> class.
        /// </summary>
        /// <param name="apiEndpoint">
        /// The base URI for the API endpoint.
        /// </param>
        /// <param name="apiKey">
        /// The API key used to authenticate against the endpoint.
        /// </param>
        public FreshdeskService(
            Uri    apiEndpoint,
            string apiKey
        )
        {
            Endpoint = new FreshdeskEndpoint(this, apiEndpoint, apiKey);
        }


        /// <summary>
        /// Gets an agent from Freshdesk.
        /// </summary>
        /// <param name="id">
        /// The ID of the agent.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The agent associated with the specified ID.
        /// </returns>
        public async Task<Agent> GetAgent(
            long                    id,
            params FreshdeskQuery[] queries
        )
        {
            return (Agent) await Endpoint.GetItem(
                FreshdeskObjectKind.Agent,
                id,
                queries
            );
        }

        /// <summary>
        /// Gets agents from Freshdesk.
        /// </summary>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The agents that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{Agent}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Agent>> GetAgents(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Agent,
                              queries
                          );

            return results.Cast<Agent>();
        }

        /// <summary>
        /// Gets companies from Freshdesk.
        /// </summary>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The companies that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{Company}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Company>> GetCompanies(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Company,
                              queries
                          );

            return results.Cast<Company>();
        }

        /// <summary>
        /// Gets a company from Freshdesk.
        /// </summary>
        /// <param name="id">
        /// The ID of the company.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The company associated with the specified ID.
        /// </returns>
        public async Task<Company> GetCompany(
            long                    id,
            params FreshdeskQuery[] queries
        )
        {
            return (Company) await Endpoint.GetItem(
                FreshdeskObjectKind.Company,
                id,
                queries
            );
        }

        /// <summary>
        /// Gets a contact from Freshdesk
        /// </summary>
        /// <param name="id">
        /// The ID of the contact.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The contact associated with the specified ID.
        /// </returns>
        public async Task<Contact> GetContact(
            long                    id,
            params FreshdeskQuery[] queries
        )
        {
            return (Contact) await Endpoint.GetItem(
                FreshdeskObjectKind.Contact,
                id,
                queries
            );
        }

        /// <summary>
        /// Gets contacts from Freshdesk.
        /// </summary>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The contacts that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{Contact}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Contact>> GetContacts(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Contact,
                              queries
                          );

            return results.Cast<Contact>();
        }

        /// <summary>
        /// Gets a ticket from Freshdesk.
        /// </summary>
        /// <param name="id">
        /// The ID of the ticket.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The ticket associated with the specified ID.
        /// </returns>
        public async Task<Ticket> GetTicket(
            long                    id,
            params FreshdeskQuery[] queries
        )
        {
            return (Ticket) await Endpoint.GetItem(
                FreshdeskObjectKind.Ticket,
                id,
                queries
            );
        }

        /// <summary>
        /// Gets conversations on a ticket from Freshdesk.
        /// </summary>
        /// <param name="ticket">
        /// The ticket.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The conversations on the specified ticket that were downloaded from
        /// Freshdesk as an <see cref="IEnumerable{Conversation}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Conversation>> GetTicketConversations(
            Ticket                  ticket,
            params FreshdeskQuery[] queries
        )
        {
            return await GetTicketConversations(ticket, queries);
        }

        /// <summary>
        /// Gets conversations on a ticket from Freshdesk.
        /// </summary>
        /// <param name="ticketId">
        /// The ID of the ticket.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The conversations on the specified ticket that were downloaded from
        /// Freshdesk as an <see cref="IEnumerable{Conversation}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Conversation>> GetTicketConversations(
            long                    ticketId,
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Ticket,
                              ticketId,
                              FreshdeskObjectKind.Conversation,
                              queries
                          );

            return results.Cast<Conversation>();
        }

        /// <summary>
        /// Gets tickets from Freshdesk.
        /// </summary>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The tickets that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{Ticket}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Ticket>> GetTickets(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Ticket,
                              queries
                          );

            return results.Cast<Ticket>();
        }

        /// <summary>
        /// Gets time entries on a ticket from Freshdesk.
        /// </summary>
        /// <param name="ticket">
        /// The ticket.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The time entries on the specified ticket that were downloaded from
        /// Freshdesk as an <see cref="IEnumerable{TicketTimeEntry}"/> collection.
        /// </returns>
        public async Task<IEnumerable<TicketTimeEntry>> GetTicketTimeEntries(
            Ticket                  ticket,
            params FreshdeskQuery[] queries
        )
        {
            return await GetTicketTimeEntries(ticket.Id, queries);
        }

        /// <summary>
        /// Gets time entries on a ticket from Freshdesk.
        /// </summary>
        /// <param name="ticketId">
        /// The ID of the ticket.
        /// </param>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The time entries on the specified ticket that were downloaded from
        /// Freshdesk as an <see cref="IEnumerable{TicketTimeEntry}"/> collection.
        /// </returns>
        public async Task<IEnumerable<TicketTimeEntry>> GetTicketTimeEntries(
            long                    ticketId,
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.Ticket,
                              ticketId,
                              FreshdeskObjectKind.TimeEntry,
                              queries
                          );

            return results.Cast<TicketTimeEntry>();
        }

        /// <summary>
        /// Gets time entries from Freshdesk.
        /// </summary>
        /// <param name="queries">
        /// An array of queries for the request.
        /// </param>
        /// <returns>
        /// The time entries that were downloaded from Freshdesk as an
        /// <see cref="IEnumerable{TicketTimeEntry}"/> collection.
        /// </returns>
        public async Task<IEnumerable<TicketTimeEntry>> GetTimeEntries(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(
                              FreshdeskObjectKind.TimeEntry,
                              queries
                          );

            return results.Cast<TicketTimeEntry>();
        }
    }
}
