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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk ticket.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Ticket : FreshdeskObject
    {
        // TODO: Add attachments property

        /// <summary>
        /// Gets or sets the ID of the agent to whom the ticket has been assigned.
        /// </summary>
        [JsonProperty("responder_id")]
        public string AssignedAgentId { get; set; }

        /// <summary>
        /// Gets the ID of the company to which this ticket belongs.
        /// </summary>
        /// <remarks>
        /// Technically this property is not read-only if you have the Estate plan, however I have no way of testing the
        /// Multiple Companies feature.
        /// </remarks>
        [JsonProperty("company_id", NullValueHandling = NullValueHandling.Ignore)]
        public long CompanyId { get; private set; }
        
        /// <summary>
        /// Gets or sets the email address(es) added in the 'cc' field of the incoming ticket email.
        /// </summary>
        [JsonProperty("cc_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] CopiedInRecipients { get; set; }
        
        /// <summary>
        /// Gets the email address(es) added while replying to a ticket.
        /// </summary>
        [JsonProperty("reply_cc_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] CopiedInRecipientsOnReply { get; private set; }

        /// <summary>
        /// Gets the key-value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; private set; }

        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public override FreshdeskObjectKind DataType
        {
            get { return FreshdeskObjectKind.Ticket; }
        }

        /// <summary>
        /// Gets the content of the ticket in plain-text.
        /// </summary>
        [JsonProperty("description_text")]
        public string Description { get; private set; }

        /// <summary>
        /// Gets or sets the timestamp that denotes when the ticket is due to be resolved.
        /// </summary>
        [JsonProperty("due_by")]
        public DateTime DueTime { get; set; }

        /// <summary>
        /// Gets or sets the email address of the requester.
        /// </summary>
        /// <remarks>
        /// If no contact exists with this email address in Freshdesk, it will be added as a new contact.
        /// </remarks>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the ID of email config which is used for this ticket.
        /// </summary>
        [JsonProperty("email_config_id", NullValueHandling = NullValueHandling.Ignore)]
        public long EmailConfigId { get; set; }

        /// <summary>
        /// Gets or sets the Facebook ID of the requester.
        /// </summary>
        /// <remarks>
        /// A contact should exist with this facebook_id in Freshdesk.
        /// </remarks>
        [JsonProperty("facebook_id")]
        public string FacebookId { get; set; }

        /// <summary>
        /// Gets or sets the timestamp that denotes when the first response is due.
        /// </summary>
        [JsonProperty("fr_due_by")]
        public DateTime FirstResponseDueTime { get; set; }

        /// <summary>
        /// Gets whether the ticket has been escalated as the result of first response time being breached.
        /// </summary>
        [JsonProperty("fr_escalated")]
        public bool FirstResponseEscalated { get; private set; }

        /// <summary>
        /// Gets the email address(es) added while forwarding a ticket.
        /// </summary>
        [JsonProperty("fwd_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] ForwardeeEmails { get; private set; }

        /// <summary>
        /// Gets or sets the ID of the group to which the ticket has been assigned.
        /// </summary>
        [JsonProperty("group_id", NullValueHandling = NullValueHandling.Ignore)]
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the HTML content of the ticket.
        /// </summary>
        [JsonProperty("description")]
        public string HtmlDescription { get; set; }

        /// <summary>
        /// Determines whether this ticket has been deleted.
        /// </summary>
        /// <remarks>
        /// Deleted tickets will not be displayed in any views except the "deleted" filter.
        /// </remarks>
        [JsonProperty("deleted")]
        public bool IsDeleted { get; private set; }
        
        /// <summary>
        /// Gets whether the ticket has been escalated for any reason.
        /// </summary>
        [JsonProperty("is_escalated")]
        public bool IsEscalated { get; private set; }

        /// <summary>
        /// Gets or sets the phone number of the requester.
        /// </summary>
        /// <remarks>
        /// If no contact exists with this phone number in Freshdesk, it will be added as a new contact. If the phone number is set and the email address is not, then the name attribute is mandatory.
        /// </remarks>
        [JsonProperty("phone")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the priority of the ticket.
        /// </summary>
        [JsonProperty("priority")]
        public TicketPriority Priority { get; set; }

        /// <summary>
        /// Gets or sets the ID of the product to which the ticket is associated.
        /// </summary>
        [JsonProperty("product_id", NullValueHandling = NullValueHandling.Ignore)]
        public long ProductId { get; set; }

        /// <summary>
        /// Gets the email addresses to which the ticket was originally sent.
        /// </summary>
        [JsonProperty("to_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Recipients { get; private set; }

        /// <summary>
        /// Gets or sets the user ID of the requester.
        /// </summary>
        [JsonProperty("requester_id")]
        public long RequesterId { get; set; }

        /// <summary>
        /// Gets or sets the name of the requester.
        /// </summary>
        [JsonProperty("name")]
        public string RequesterName { get; set; }

        /// <summary>
        /// Gets or sets the channel through which the ticket was created.
        /// </summary>
        [JsonProperty("source")]
        public TicketSource Source { get; set; }

        /// <summary>
        /// Gets whether the ticket has been marked as spam.
        /// </summary>
        [JsonProperty("spam")]
        public bool Spam { get; private set; }

        /// <summary>
        /// Gets or sets the status of the ticket.
        /// </summary>
        [JsonProperty("status")]
        public int Status { get; set; }

        /// <summary>
        /// Gets or sets the subject of the ticket.
        /// </summary>
        [JsonProperty("subject")]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the tags that have been associated with the ticket.
        /// </summary>
        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        /// <summary>
        /// Gets or sets the Twitter handle of the requester.
        /// </summary>
        /// <remarks>
        /// If no contact exists with this handle in Freshdesk, it will be added as a new contact.
        /// </remarks>
        [JsonProperty("twitter_id")]
        public string TwitterId { get; set; }

        /// <summary>
        /// Gets or sets the issue category that describes the ticket.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        
        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this ticket.
        /// </summary>
        private FreshdeskService FreshdeskConnection { get; set; }

        /// <summary>
        /// The conversations of this ticket retrieved from JSON.
        /// </summary>
        [JsonProperty("conversations")]
        private Conversation[] JsonConversations { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class.
        /// </summary>
        public Ticket() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class from JSON
        /// source data.
        /// </summary>
        /// <param name="json">
        /// The JSON to deserialize from.
        /// </param>
        /// <param name="fdConn">
        /// The Freshdesk connection used to acquire this ticket.
        /// </param>
        public Ticket(
            string json,
            FreshdeskService fdConn = null
        )
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ticket"/> class from a JSON
        /// object.
        /// </summary>
        /// <param name="obj">
        /// The JSON object.
        /// </param>
        /// <param name="fdConn">
        /// The Freshdesk connection used to acquire this ticket.
        /// </param>
        public Ticket(
            JObject obj,
            FreshdeskService fdConn = null
        )
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            FreshdeskConnection = fdConn;
        }


        /// <summary>
        /// Gets the conversations on this ticket.
        /// </summary>
        /// <param name="page">
        /// The page number.
        /// </param>
        /// <returns>
        /// The conversations on this ticket as an
        /// <see cref="IEnumerable{Conversation}"/> collection.
        /// </returns>
        public async Task<IEnumerable<Conversation>> GetConversations(
            int page
        )
        {
            if (JsonConversations != null)
            {
                return JsonConversations;
            }

            if (Freshdesk == null)
            {
                throw new InvalidOperationException("Ticket.InitializeConversations: No Freshdesk connection has been provided for this ticket.");
            }
            
            return await Freshdesk.GetTicketConversations(
                Id,
                new PaginationQuery(page)
            );
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return string.Format(
                "#{0} - {1}",
                Id,
                Subject
            );
        }
    }
}
