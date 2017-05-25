using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Freshdesk.Schema
{
    /// <summary>
    /// Represents a Freshdesk ticket.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Ticket
    {
        // TODO: Add attachments property

        /// <summary>
        /// Gets or sets the ID of the agent to whom the ticket has been assigned.
        /// </summary>
        [JsonProperty("responder_id")]
        public string AssignedAgentId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the company to which this ticket belongs.
        /// 
        /// This property is read-only.
        /// </summary>
        /// <remarks>
        /// Technically this property is not read-only if you have the Estate plan, however I have no way of testing the
        /// Multiple Companies feature.
        /// </remarks>
        [JsonProperty("company_id")]
        public long CompanyId
        {
            get { return _CompanyId; }
            set { if (!ReadOnlyLocked) _CompanyId = value; }
        }
        private long _CompanyId;

        /// <summary>
        /// Gets or sets the email address(es) added in the 'cc' field of the incoming ticket email.
        /// </summary>
        [JsonProperty("cc_emails")]
        public string[] CopiedInRecipients { get; set; }

        /// <summary>
        /// Gets or sets the email address(es) added while replying to a ticket.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("reply_cc_emails")]
        public string[] CopiedInRecipientsOnReply
        {
            get { return _CopiedInRecipientsOnReply; }
            set { if (!ReadOnlyLocked) _CopiedInRecipientsOnReply = value; }
        }
        private string[] _CopiedInRecipientsOnReply;

        /// <summary>
        /// Gets or sets the ticket creation timestamp.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("created_at")]
        public DateTime CreatedAt
        {
            get { return _CreatedAt; }
            set { if (!ReadOnlyLocked) _CreatedAt = value; }
        }
        private DateTime _CreatedAt;

        /// <summary>
        /// Gets or sets the key-value pairs containing the names and values of custom fields.
        /// </summary>
        [JsonProperty("custom_fields")]
        public Dictionary<string, object> CustomFields { get; set; }

        /// <summary>
        /// Gets or sets whether the ticket has been deleted/trashed.
        /// </summary>
        /// <remarks>
        /// Deleted tickets will not be displayed in any views except the "deleted" filter.
        /// </remarks>
        [JsonProperty("deleted")]
        public bool Deleted
        {
            get { return _Deleted; }
            set { if (!ReadOnlyLocked) _Deleted = value; }
        }
        private bool _Deleted;

        /// <summary>
        /// Gets or sets the content of the ticket in plain-text.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("description_text")]
        public string Description
        {
            get { return _Description; }
            set { if (!ReadOnlyLocked) _Description = value; }
        }
        private string _Description;

        /// <summary>
        /// Gets or sets the HTML content of the ticket.
        /// </summary>
        [JsonProperty("description")]
        public string HtmlDescription { get; set; }

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
        [JsonProperty("email_config_id")]
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
        /// Gets or sets whether the ticket has been escalated as the result of first response time being breached.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("fr_escalated")]
        public bool FirstResponseEscalated
        {
            get { return _FirstResponseEscalated; }
            set { if (!ReadOnlyLocked) _FirstResponseEscalated = value; }
        }
        private bool _FirstResponseEscalated;

        /// <summary>
        /// Gets or sets the email address(es) added while forwarding a ticket.
        /// </summary>
        [JsonProperty("fwd_emails")]
        public string[] ForwardeeEmails
        {
            get { return _ForwardeeEmails; }
            set { if (!ReadOnlyLocked) _ForwardeeEmails = value; }
        }
        private string[] _ForwardeeEmails;

        /// <summary>
        /// Gets or sets the ID of the group to which the ticket has been assigned.
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupId { get; set; }

        /// <summary>
        /// Gets or sets the unique ID of the ticket.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("id")]
        public long Id
        {
            get { return _Id; }
            set { if (!ReadOnlyLocked) _Id = value; }
        }
        private long _Id;

        /// <summary>
        /// Gets or sets whether the ticket has been escalated for any reason.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("is_escalated")]
        public bool IsEscalated
        {
            get { return _IsEscalated; }
            set { if (!ReadOnlyLocked) _IsEscalated = value; }
        }
        private bool _IsEscalated;

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
        [JsonProperty("product_id")]
        public long ProductId { get; set; }

        /// <summary>
        /// Gets or sets the email addresses to which the ticket was originally sent.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("to_emails")]
        public string[] Recipients
        {
            get { return _Recipients; }
            set { if (!ReadOnlyLocked) _Recipients = value; }
        }
        private string[] _Recipients;

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
        /// Gets or sets whether the ticket has been marked as spam.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("spam")]
        public bool Spam
        {
            get { return _Spam;  }
            set { if (!ReadOnlyLocked) _Spam = value; }
        }
        private bool _Spam;

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
        public int Type { get; set; }

        /// <summary>
        /// Gets or sets the ticket updated timestamp.
        /// 
        /// This property is read-only.
        /// </summary>
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt
        {
            get { return _UpdatedAt; }
            set { if (!ReadOnlyLocked) _UpdatedAt = value; }
        }
        private DateTime _UpdatedAt;


        /// <summary>
        /// Gets or sets whether the read-only properties are locked.
        /// </summary>
        private bool ReadOnlyLocked { get; set; }


        /// <summary>
        /// Initializes a new instance of the Ticket class.
        /// </summary>
        public Ticket()
        {
            ReadOnlyLocked = true;
        }

        /// <summary>
        /// Initializes a new instance of the Ticket class from JSON source data.
        /// </summary>
        /// <param name="json">The JSON to deserialize from.</param>
        public Ticket(string json)
        {
            JsonConvert.PopulateObject(json, this);
            ReadOnlyLocked = true;
        }
    }
}
