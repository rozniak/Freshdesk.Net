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
