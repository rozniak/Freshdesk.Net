/*
 * Copyright 2015 Beckersoft, Inc.
 *
 * Author(s):
 *  John Becker (john@beckersoft.com)
 *  Oleg Shevchenko (shevchenko.oleg@outlook.com)
 *  Joseph Poh (github user jozsurf)
 *  (github user ninjacarr)
 *  (github user sloppypointless)
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

using Freshdesk.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Freshdesk
{
    /// <summary>
    /// Represents a connection to a Freshdesk instance, using version 2 of the Freshdesk API
    /// </summary>
    public class FreshdeskConnection
    {
        /// <summary>
        /// The Regex rule to use when validating the connection hostname.
        /// </summary>
        public static readonly Regex UriRule = new Regex("^[A-Za-z0-9]+\\.freshdesk\\.com$");


        /// <summary>
        /// Gets the connected URI.
        /// </summary>
        public Uri ConnectionUri { get; private set; }


        /// <summary>
        /// Initializes a new instance of the FreshdeskConnection class.
        /// </summary>
        /// <param name="apiKey">The API key to authenticate with.</param>
        /// <param name="connUri">The URI of the Freshdesk instance.</param>
        public FreshdeskConnection(string apiKey, Uri connUri)
        {
            FreshHttpsHelper.AuthorizationKey = apiKey;

            // Validate the URI
            if (connUri.Scheme != "https" ||
                !UriRule.IsMatch(connUri.Host) ||
                connUri.PathAndQuery != "/")
                throw new ArgumentException("FreshdeskConnection.New: Invalid connection URI provided. URI must be in the form https://*.freshdesk.com.\n\nSee https://developer.freshdesk.com/api/#authentication for details.");

            ConnectionUri = connUri;

            // Force TLS 1.1 or higher. Anything lower is deprecated in the Freshdesk API as of 2016-09-30
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls11;
        }


        /// <summary>
        /// Creates a new ticket on the helpdesk.
        /// </summary>
        /// <param name="ticket">The ticket to create.</param>
        /// <returns>The resultant Ticket object that has been finalized on Freshdesk.</returns>
        public async Task<Ticket> CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("FreshdeskConnection.CreateTicket: Parameter 'ticket' cannot be null.");

            //return (Ticket) await FreshHttpsHelper.DoRequest<Ticket>(FreshHttpsHelper.UriForPath(ConnectionUri, "/api/v2/tickets"), "POST", JsonConvert.SerializeObject(ticket));
            return null;
        }

        /// <summary>
        /// Delete a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>True if the ticket was deleted or does not exist.</returns>
        public async Task<bool> DeleteTicket(long id)
        {
            // TODO: Code this
            return false;
        }

        /// <summary>
        /// Gets a list of tickets from the global ticket list.
        /// </summary>
        /// <returns>A list of tickets from the first page, with a maximum of 30 results, as an IList&lt;Ticket&gt; collection.</returns>
        public async Task<IList<Ticket>> GetAllTickets()
        {
            return await GetAllTickets(1, 30);
        }

        /// <summary>
        /// Gets a list of tickets from the global ticket list.
        /// </summary>
        /// <param name="page">The page number.</param>
        /// <param name="quantity">The max number of tickets to return on a given page. The maximum Freshdesk will accept is 100.</param>
        /// <returns>A list of tickets from the specified page, with a specified maximum amount of results, as an IList&lt;Ticket&gt; collection.</returns>
        public async Task<IList<Ticket>> GetAllTickets(int page, int quantity = 30)
        {
            if (quantity < 1 || quantity > 100)
                throw new ArgumentOutOfRangeException("FreshdeskConnection.GetAllTickets: Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive.");

            if (page < 1)
                throw new ArgumentOutOfRangeException("FreshdeskConnection.GetAllTickets: Parameter 'page' out of range, value must be 1 or greater.");

            var result = (IList<Ticket>)await FreshHttpsHelper.DoRequest<IList<Ticket>>(FreshHttpsHelper.UriForPath(ConnectionUri, "/api/v2/tickets",
                "page=" + page.ToString() + "&per_page=" + quantity.ToString()));

            return new List<Ticket>(result).AsReadOnly();
        }

        /// <summary>
        /// Gets a ticket from the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The ID of the ticket.</param>
        /// <returns>A Ticket object populated with ticket data retrieved from Freshdesk if the ID was found, null otherwise.</returns>
        public async Task<Ticket> GetTicketById(long id)
        {
            // TODO: Code this
            return null;
        }
        
        /// <summary>
        /// Gets a list of tickets from a company on the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <returns>A list of the company's tickets from the first page, with a maximum of 30 results, as an IList&lt;Ticket&gt; collection.</returns>
        public async Task<IList<Ticket>> GetTicketsByCompany(long id)
        {
            return await GetTicketsByCompany(id, 1, 30);
        }

        /// <summary>
        /// Gets a list of tickets from a company on the helpdesk by its ID.
        /// </summary>
        /// <param name="id">The company ID.</param>
        /// <param name="page">The page number.</param>
        /// <param name="quantity">The max number of tickets to return on a given page. The maximum Freshdesk will accept is 100.</param>
        /// <returns>A list of the company's tickets from the specified page, with a specified maximum amount of results, as an IList&lt;Ticket&gt; collection.</returns>
        public async Task<IList<Ticket>> GetTicketsByCompany(long id, int page, int quantity = 30)
        {
            if (quantity < 1 || quantity > 100)
                throw new ArgumentOutOfRangeException("FreshdeskConnection.GetTicketsByCompany: Parameter 'quantity' out of range, accepted values are between 1 and 100 inclusive.");

            if (page < 1)
                throw new ArgumentOutOfRangeException("FreshdeskConnection.GetTicketsByCompany: Parameter 'page' out of range, value must be 1 or greater.");

            var result = (IList<Ticket>) await FreshHttpsHelper.DoRequest<IList<Ticket>>(FreshHttpsHelper.UriForPath(ConnectionUri, "/api/v2/tickets",
                "company_id=" + id.ToString() + "&page=" + page.ToString() + "&per_page=" + quantity.ToString()));

            return new List<Ticket>(result).AsReadOnly();
        }


        

  //      #region Customers
  //      /// <summary>
  //      /// Creates a Company
  //      /// </summary>
  //      /// <param name="createCustomerRequest"></param>
  //      /// <returns></returns>
  //      public GetCustomerResponse CreateCustomer(CreateCustomerRequest createCustomerRequest)
  //      {
  //          if (createCustomerRequest == null)
  //          {
  //              throw new ArgumentNullException("createCustomerRequest");
  //          }

  //          return FreshHttpsHelper.DoRequest<GetCustomerResponse>(FreshHttpsHelper.UriForPath(ConnectionUri, "/customers.json"), "POST", JsonConvert.SerializeObject(createCustomerRequest));
  //      }
  //      #endregion

  //      #region Tickets
        


  //      /// <summary>
  //      /// Creates a Support Ticket
  //      /// </summary>
  //      /// <param name="createTicketRequest"></param>
  //      /// <returns></returns>
  //      public GetTicketResponse CreateTicket(CreateTicketRequest createTicketRequest) {
  //          if (createTicketRequest == null)
  //          {
  //              throw new ArgumentNullException("createTicketRequest");
  //          }
  //          
  //      }

  //      /// <summary>
  //      /// Creates a Support Ticket with an attachment
  //      /// </summary>
  //      /// <param name="createTicketRequest"></param>
  //      /// <param name="attachments"></param>
  //      /// <returns></returns>
  //      public GetTicketResponse CreateTicketWithAttachment(CreateTicketRequest createTicketRequest, IEnumerable<Attachment> attachments) {
  //          if (createTicketRequest == null)
  //          {
  //              throw new ArgumentNullException("createTicketRequest");
  //          }
  //          if (attachments == null)
  //          {
  //              throw new ArgumentNullException("attachments");
  //          }

  //          return FreshHttpsHelper.DoMultipartFormRequest<GetTicketResponse>(FreshHttpsHelper.UriForPath(ConnectionUri, "/helpdesk/tickets.json"), createTicketRequest, attachments, "helpdesk_ticket", "helpdesk_ticket[attachments][][resource]");
  //      }

  //      /// <summary>
  //      /// Gets a list of Support Tickets for a company by name
  //      /// </summary>
  //      /// <param name="companyName"></param>
  //      /// <returns></returns>
  //      public GetTicketListItemResponse[] GetTicketsByCompany(string companyName)
  //      {
  //          if (string.IsNullOrEmpty(companyName))
  //          {
  //              throw new ArgumentNullException("companyName");
  //          }

  //          return FreshHttpsHelper.DoRequest<GetTicketListItemResponse[]>(FreshHttpsHelper.UriForPath(ConnectionUri, "/helpdesk/tickets.json", "company_name=" + companyName + "&filter_name=all_tickets"));
  //      }
  //      #endregion

  //      #region Users
  //      /// <summary>
  //      /// Create Contact
  //      /// </summary>
  //      /// <param name="createUserRequest"></param>
  //      /// <returns></returns>
  //      public GetUserResponse CreateUser(CreateUserRequest createUserRequest) {
  //          if (createUserRequest == null) {
  //              throw new ArgumentNullException("createUserRequest");
  //          }
  //          return FreshHttpsHelper.DoRequest<GetUserResponse>(FreshHttpsHelper.UriForPath(ConnectionUri, "/contacts.json"), "POST", JsonConvert.SerializeObject(createUserRequest));
  //      }

  //      /// <summary>
  //      /// Update a contact
  //      /// </summary>
  //      /// <param name="updateUserRequest"></param>
  //      /// <param name="id"></param>
  //      public void UpdateUser(UpdateUserRequest updateUserRequest, long id) {
  //          if (updateUserRequest == null) {
  //              throw new ArgumentNullException("updateUserRequest");
  //          }
  //          FreshHttpsHelper.DoRequest<string>(FreshHttpsHelper.UriForPath(ConnectionUri, string.Format("/contacts/{0}.json", id)), "PUT", JsonConvert.SerializeObject(updateUserRequest));
  //      }

  //      /// <summary>
  //      /// Get users
  //      /// </summary>
  //      /// <returns></returns>
		//public IEnumerable<GetUserRequest> GetUsers() {

  //          var users = new List<GetUserRequest>();
  //          var page = 1;
  //          while (true)
  //          {
  //              var paginatedUsers = FreshHttpsHelper.DoRequest<IEnumerable<GetUserRequest>>(FreshHttpsHelper.UriForPath(ConnectionUri, "/contacts.json", string.Format("page={0}", page))).ToList();

  //              if (paginatedUsers.Any())
  //              {
  //                  users.AddRange(paginatedUsers);
  //                  page++;
  //              }
  //              else
  //              {
  //                  break;
  //              }
  //          }
  //          return users;
  //      }


  //      #endregion

  //      #region Time Entries
  //      /// <summary>
  //      /// Creates a Time Entry
  //      /// </summary>
  //      /// <param name="createTimeRequest"></param>
  //      /// <returns></returns>
  //      public GetTimeResponse CreateTimeEntry(CreateTimeRequest createTimeRequest, int ticket)
  //      {

  //          if (createTimeRequest == null)
  //          {
  //              throw new ArgumentNullException("createTimeRequest");
  //          }
  //          return FreshHttpsHelper.DoRequest<GetTimeResponse>(FreshHttpsHelper.UriForPath(ConnectionUri, "/helpdesk/tickets/" + ticket.ToString() + "/time_sheets.json"), "POST", JsonConvert.SerializeObject(createTimeRequest));
  //      }


  //      #endregion

    }

}
