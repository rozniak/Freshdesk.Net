/*
 * Freshdesk.FreshdeskConnection -- Main Freshdesk API Implementation
 *
 * This source-code is part of the Freshdesk API for C# library by Rory Fewell (rozniak) of Oddmatics for Agile ICT for Education Ltd.:
 * <<https://oddmatics.uk>>
 * <<http://www.agileict.co.uk>>
 * 	
 * Copyright (C) 2017 Oddmatics
 * 	
 * Sharing, editing and general licence term information can be found inside of the "LICENSE.MD" file that should be located in the root of this project's directory structure.
 */

using Freshdesk.Internal;
using Freshdesk.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Freshdesk
{
    /// <summary>
    /// Provides an interface to the public Freshdesk API.
    /// </summary>
    public class FreshdeskConnection
    {
        /// <summary>
        /// Gets or sets the URI of the API endpoint.
        /// </summary>
        public Uri ApiEndpoint
        {
            get { return Endpoint.BaseUri; }
        }


        /// <summary>
        /// The communication layer for performing the API calls to Freshdesk.
        /// </summary>
        private FreshdeskEndpoint Endpoint { get; set; }


        /// <summary>
        /// Initializes a new instance of the FreshdeskConnection class.
        /// </summary>
        /// <param name="apiEndpoint">
        /// The base URI for the API endpoint.
        /// </param>
        /// <param name="apiKey">
        /// The API key used to authenticate against the endpoint.
        /// </param>
        public FreshdeskConnection(
            Uri    apiEndpoint,
            string apiKey
        )
        {
            Endpoint = new FreshdeskEndpoint(this, apiEndpoint, apiKey);

            // TODO: Bin once FreshHttpsHelper has been replaced by better code
            //
            FreshHttpsHelper.AuthorizationKey = apiKey;

            // Use TLS 1.2. Anything lower is deprecated in the Freshdesk API as of
            // 2020-04-30
            //
            // FIXME: Really this should probably be set by the application and not
            //        this library...?
            //
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }


        /// <summary>
        /// Creates a new ticket on the helpdesk.
        /// </summary>
        /// <param name="ticket">
        /// The ticket to create.
        /// </param>
        /// <returns>
        /// The resultant Ticket object that has been finalized on Freshdesk.
        /// </returns>
        public async Task<Ticket> CreateTicket(
            Ticket ticket
        )
        {
            if (ticket == null)
            {
                throw new ArgumentNullException(
                    "Parameter 'ticket' cannot be null."
                );
            }

            //return (Ticket) await FreshHttpsHelper.DoRequest<Ticket>(FreshHttpsHelper.UriForPath(ConnectionUri, "/api/v2/tickets"), "POST", JsonConvert.SerializeObject(ticket));
            return null;
        }

        /// <summary>
        /// Delete a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the ticket.
        /// </param>
        /// <returns>
        /// True if the ticket was deleted or does not exist.
        /// </returns>
        public async Task<bool> DeleteTicket(
            long id
        )
        {
            // TODO: Code this
            return false;
        }

        /// <summary>
        /// Gets an agent from the helpdesk by their ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the agent.
        /// </param>
        /// <returns>
        /// An Agent object populated with company data retrieved from Freshdesk if the
        /// ID was found, null otherwise.
        /// </returns>
        public async Task<Agent> GetAgent(
            long id
        )
        {
            return (Agent) await FreshHttpsHelper.DoRequest<Agent>(
                FreshHttpsHelper.UriForPath(
                    ApiEndpoint,
                    "/api/v2/agents/" + id.ToString()
                ),
                this
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
        /// <see cref="IList{Agent}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Agent>> GetAgents(
            params FreshdeskQuery[] queries
        )
        {
            var results = await Endpoint.GetItems(FreshdeskObjectKind.Agent, queries);

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
        /// Gets a company from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the company.
        /// </param>
        /// <returns>
        /// A Company object populated with company data retrieved from Freshdesk if
        /// the ID was found, null otherwise.
        /// </returns>
        public async Task<Company> GetCompany(
            long id
        )
        {
            return (Company) await FreshHttpsHelper.DoRequest<Company>(
                FreshHttpsHelper.UriForPath(
                    ApiEndpoint,
                    "/api/v2/companies/" + id.ToString()
                ),
                this
            );
        }

        /// <summary>
        /// Gets a contact from the helpdesk by their ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the contact.
        /// </param>
        /// <returns>
        /// A Contact object populated with contact data retrieved from Freshdesk if
        /// the ID was found, null otherwise.
        /// </returns>
        public async Task<Contact> GetContact(
            long id
        )
        {
            return (Contact) await FreshHttpsHelper.DoRequest<Contact>(
                FreshHttpsHelper.UriForPath(
                    ApiEndpoint,
                    "/api/v2/contacts/" + id.ToString()
                ),
                this
            );
        }

        /// <summary>
        /// Gets a list of contacts from the helpdesk.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="applyDeletedFilter">
        /// True to filter this request to deleted contacts only.
        /// </param>
        /// <param name="quantity">
        /// The max number of contacts to return on a given page. The maximum Freshdesk
        /// will accept is 100.
        /// </param>
        /// <returns>
        /// A list of contacts from the specified page, with a specified maximum amount
        /// of results, as an IList&lt;Contact&gt; collection.
        /// </returns>
        public async Task<IList<Contact>> GetContacts(
            int  page,
            bool applyDeletedFilter,
            int  quantity = 30
        )
        {
            if (quantity < 1 || quantity > 100)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive."
                );
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'page' out of range, value must be 1 or greater."
                );
            }

            var result =
                (IList<object>)await FreshHttpsHelper.DoRequest<IList<Contact>>(
                    FreshHttpsHelper.UriForPath(
                        ApiEndpoint,
                        "/api/v2/contacts",
                        "page=" + page.ToString() + "&per_page=" + quantity.ToString() + (applyDeletedFilter ? "&state=deleted" : "")
                    ),
                    this
                );

            return CastReadOnlyList<Contact>(result);
        }

        /// <summary>
        /// Gets a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the ticket.
        /// </param>
        /// <returns>
        /// A Ticket object populated with ticket data retrieved from Freshdesk if the
        /// ID was found, null otherwise.
        /// </returns>
        public async Task<Ticket> GetTicket(
            long id
        )
        {
            return (Ticket) await FreshHttpsHelper.DoRequest<Ticket>(
                FreshHttpsHelper.UriForPath(
                    ApiEndpoint,
                    "/api/v2/tickets/" + id.ToString(), "include=company,requester"
                ),
                this
            );
        }

        /// <summary>
        /// Gets all conversations on a ticket from the helpdesk by the ticket's ID.
        /// </summary>
        /// <param name="ticketId">
        /// The ID of the ticket.
        /// </param>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <returns>
        /// A list of conversations from the specified ticket as an
        /// IList&lt;Conversation&gt; collection.
        /// </returns>
        public async Task<IList<Conversation>> GetTicketConversations(
            long ticketId,
            int  page
        )
        {
            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'page' out of range, value must be 1 or greater."
                );
            }
            
            var result =
                (IList<object>) await FreshHttpsHelper.DoRequest<IList<Conversation>>(
                    FreshHttpsHelper.UriForPath(
                        ApiEndpoint,
                        "/api/v2/tickets/" + ticketId.ToString() + "/conversations",
                        "page=" + page
                    ),
                    this
                );

            return CastReadOnlyList<Conversation>(result);
        }

        /// <summary>
        /// Gets a list of tickets from the helpdesk ticket list.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="applyDeletedFilter">
        /// True to filter this request to deleted tickets only.
        /// </param>
        /// <param name="quantity">
        /// The max number of tickets to return on a given page. The maximum Freshdesk
        /// will accept is 100.
        /// </param>
        /// <returns>
        /// A list of tickets from the specified page, with a specified maximum amount
        /// of results, as an IList&lt;Ticket&gt; collection.
        /// </returns>
        public async Task<IList<Ticket>> GetTickets(
            int  page,
            bool applyDeletedFilter,
            int  quantity = 30)
        {
            return await GetTickets(
                page,
                applyDeletedFilter,
                new DateTime(2000, 1, 1),
                quantity
            );
        }

        /// <summary>
        /// Gets a list of tickets from the helpdesk ticket list.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="applyDeletedFilter">
        /// True to filter this request to deleted tickets only.
        /// </param>
        /// <param name="updatedSinceFilter">
        /// The date and time of the earliest "last updated" ticket to filter by.
        /// </param>
        /// <param name="quantity">
        /// The max number of tickets to return on a given page. The maximum Freshdesk
        /// will accept is 100.
        /// </param>
        /// <returns>
        /// A list of tickets from the specified page, with a specified maximum amount
        /// of results, as an IList&lt;Ticket&gt; collection.
        /// </returns>
        public async Task<IList<Ticket>> GetTickets(
            int page,
            bool applyDeletedFilter,
            DateTime updatedSinceFilter,
            int quantity = 30
        )
        {
            if (quantity < 1 || quantity > 100)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive."
                );
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'page' out of range, value must be 1 or greater."
                );
            }

            string req = "page=" + page.ToString() + "&per_page=" + quantity.ToString() + "&updated_since=" + updatedSinceFilter.ToUniversalTime().ToString("s") + "Z" + (applyDeletedFilter ? "&filter=deleted" : "");

            var result =
                (IList<object>)await FreshHttpsHelper.DoRequest<IList<Ticket>>(
                    FreshHttpsHelper.UriForPath(
                        ApiEndpoint,
                        "/api/v2/tickets",
                        req
                    ),
                    this
                );

            return CastReadOnlyList<Ticket>(result);
        }

        /// <summary>
        /// Gets a list of tickets from a company on the helpdesk by its ID.
        /// </summary>
        /// <param name="id">
        /// The company ID.
        /// </param>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="quantity">
        /// The max number of tickets to return on a given page. The maximum Freshdesk
        /// will accept is 100.
        /// </param>
        /// <returns>
        /// A list of the company's tickets from the specified page, with a specified
        /// maximum amount of results, as an IList&lt;Ticket&gt; collection.
        /// </returns>
        public async Task<IList<Ticket>> GetTicketsByCompany(
            long id,
            int  page,
            int  quantity = 30
        )
        {
            if (quantity < 1 || quantity > 100)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive."
                );
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'page' out of range, value must be 1 or greater."
                );
            }

            var result =
                (IList<object>) await FreshHttpsHelper.DoRequest<IList<Ticket>>(
                    FreshHttpsHelper.UriForPath(
                        ApiEndpoint,
                        "/api/v2/tickets",
                        "company_id=" + id.ToString() + "&page=" + page.ToString() + "&per_page=" + quantity.ToString() + "&updated_since=2000-01-01T01:00:00Z"
                    ),
                    this
                );

            return CastReadOnlyList<Ticket>(result);
        }

        /// <summary>
        /// Gets a list of time entries from the helpdesk time entries list.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <param name="quantity">
        /// The max number of time enrties to return on a given page. The maximum
        /// Freshdesk will accept is 100.
        /// </param>
        /// <returns>
        /// A list of the company's time entries from the specified page, with a
        /// specified maximum amount of results, as an IList&lt;TicketTimeEntry&gt;
        /// collection.
        /// </returns>
        public async Task<IList<TicketTimeEntry>> GetTimeEntries(
            int page,
            int quantity = 30
        )
        {
            if (quantity < 1 || quantity > 100)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive."
                );
            }

            if (page < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "Parameter 'page' out of range, value must be 1 or greater."
                );
            }

            var result =
                (IList<object>) await FreshHttpsHelper.DoRequest<IList<TicketTimeEntry>>(
                    FreshHttpsHelper.UriForPath(
                        ApiEndpoint,
                        "/api/v2/time_entries",
                        "page=" + page.ToString() + "&per_page=" + quantity.ToString() + "&executed_after=2000-01-01T01:00:00Z"
                    ),
                    this
                );

            return CastReadOnlyList<TicketTimeEntry>(result);
        }


        /// <summary>
        /// Helper method for casting IList&lt;object&gt; collections to a read-only
        /// collection of the specified Type.
        /// </summary>
        /// <typeparam name="T">
        /// The Type to cast to within the resulting collection.
        /// </typeparam>
        /// <param name="objCollection">
        /// The IList&lt;object&gt; collection.
        /// </param>
        /// <returns>
        /// A read-only IList&lt;&gt; collection with generic type T.
        /// </returns>
        private IList<T> CastReadOnlyList<T>(
            IList<object> objCollection
        )
        {
            var castedResult = from obj in objCollection
                               select (T)obj;

            return new List<T>(castedResult).AsReadOnly();
        }
    }

}
