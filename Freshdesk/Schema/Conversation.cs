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

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a conversation on a ticket.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public sealed class Conversation : FreshdeskObject
    {
        /// <summary>
        /// Gets the collection of email addresses that are in the BCC field of the outgoing ticket email.
        /// </summary>
        public IList<string> BccEmails
        {
            get
            {
                return (Id == -1 ? (IList<string>)_CustomBccEmails : _JsonBccEmails.AsReadOnly());
            }
        }

        private List<string> _CustomBccEmails;

        [JsonProperty("bcc_emails")]
        private List<string> _JsonBccEmails;


        /// <summary>
        /// Gets the content of the conversation in plain-text.
        /// </summary>
        [JsonProperty("body_text")]
        public string Body { get; private set; }

        /// <summary>
        /// Gets the collection of email addresses that are in the CC field of the outgoing ticket email.
        /// </summary>
        public IList<string> CcEmails
        {
            get
            {
                return (Id == -1 ? (IList<string>)_CustomCcEmails : _JsonCcEmails.AsReadOnly());
            }
        }

        private List<string> _CustomCcEmails;

        [JsonProperty("cc_emails")]
        private List<string> _JsonCcEmails;


        /// <summary>
        /// Gets or sets the data type of the object.
        /// </summary>
        public override FreshdeskObjectKind DataType
        {
            get { return FreshdeskObjectKind.Conversation; }
        }

        /// <summary>
        /// Gets or sets the HTML content of the conversation.
        /// </summary>
        [JsonProperty("body")]
        public string HtmlBody { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether this conversation should appear as though it was created from outside of Freshdesk.
        /// </summary>
        [JsonProperty("incoming")]
        public bool Incoming { get; set; }

        /// <summary>
        /// Gets or sets the value that indicates whether this is a private note.
        /// </summary>
        [JsonProperty("private")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets the collection of users to be notified about this conversation.
        /// </summary>
        public IList<string> NotifyUsers
        {
            get
            {
                return (Id == -1 ? (IList<string>)_CustomNotifyUsers : _JsonNotifyUsers.AsReadOnly());
            }
        }

        private List<string> _CustomNotifyUsers;

        [JsonProperty("notify_emails")]
        private List<string> _JsonNotifyUsers;


        /// <summary>
        /// Gets the email addresses to which the conversation was originally sent.
        /// </summary>
        [JsonProperty("to_emails")]
        public string[] Recipients { get; private set; }

        /// <summary>
        /// Gets or sets the email address from which the reply is sent.
        /// </summary>
        public string SenderEmail
        {
            get { return _SenderEmail; }
            set
            {
                if (Id == -1)
                    _SenderEmail = value;
                else
                    throw new InvalidOperationException("Conversation.SenderEmail.set: Cannot set SenderEmail property on an existing conversation.");
            }
        }

        [JsonProperty("from_email")]
        private string _SenderEmail;


        /// <summary>
        /// Gets the type of this conversation.
        /// </summary>
        [JsonProperty("source")]
        public ConversationSource Source { get; private set; }

        /// <summary>
        /// Gets the email address from which the reply was sent.
        /// </summary>
        /// <remarks>For notes, this value is null.</remarks>
        [JsonProperty("support_email", NullValueHandling = NullValueHandling.Ignore)]
        public string SupportEmail { get; private set; }

        /// <summary>
        /// Gets the ID of the ticket to which this conversation belongs.
        /// </summary>
        [JsonProperty("ticket_id")]
        public long TicketId { get; private set; }

        /// <summary>
        /// Gets or sets the ID of the user that created this conversation.
        /// </summary>
        public long? UserId
        {
            get { return _UserId; }
            set
            {
                if (Id == -1)
                    _UserId = value;
                else
                    throw new InvalidOperationException("Conversation.UserId.set: Cannot set UserId property on an existing conversation.");
            }
        }

        [JsonProperty("user_id", NullValueHandling = NullValueHandling.Include)]
        private long? _UserId;


        /// <summary>
        /// The Freshdesk connection instance that was used to acquire this conversation.
        /// </summary>
        private FreshdeskService FreshdeskConnection { get; set; }


        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        public Conversation() { }

        /// <summary>
        /// Initializes a new instance of the Conversation class.
        /// </summary>
        /// <param name="fdConn">The Freshdesk connection used to acquire this conversation.</param>
        public Conversation(FreshdeskService fdConn = null)
        {
            FreshdeskConnection = fdConn;

            _CustomBccEmails = new List<string>();
            _CustomCcEmails = new List<string>();
            _CustomNotifyUsers = new List<string>();
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this conversation.</param>
        public Conversation(string json, FreshdeskService fdConn = null)
        {
            JsonConvert.PopulateObject(json, this);

            FreshdeskConnection = fdConn;
        }

        /// <summary>
        /// Initializes a new instance of the Conversation class from a JSON object.
        /// </summary>
        /// <param name="obj">The JSON object.</param>
        /// <param name="fdConn">The Freshdesk connection used to acquire this conversation.</param>
        public Conversation(JObject obj, FreshdeskService fdConn = null)
        {
            using (var jReader = obj.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(jReader, this);
            }

            FreshdeskConnection = fdConn;
        }


        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>
        /// A string that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Body;
        }
    }
}
