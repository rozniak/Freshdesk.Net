using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk
{
    [JsonObject(MemberSerialization.OptIn)]
    public class GetTicketListItemResponse
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "deleted")]
        public bool Deleted { get; set; }

        [JsonProperty(PropertyName = "delta")]
        public bool Delta { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "description_html")]
        public string DescriptionHtml { get; set; }

        [JsonProperty(PropertyName = "display_id")]
        public long? DisplayId { get; set; }

        [JsonProperty(PropertyName = "due_by")]
        public string DueBy { get; set; }

        [JsonProperty(PropertyName = "email_config_id")]
        public string EmailConfigId { get; set; }

        [JsonProperty(PropertyName = "frDueBy")]
        public string FrDueBy { get; set; }

        [JsonProperty(PropertyName = "fr_escalated")]
        public string FrEscalated { get; set; }

        [JsonProperty(PropertyName = "group_id")]
        public long? GroupId { get; set; }

        [JsonProperty(PropertyName = "id")]
        public long? Id { get; set; }

        [JsonProperty(PropertyName = "isescalated")]
        public bool IsEscalated { get; set; }

        [JsonProperty(PropertyName = "owner_id")]
        public long? OwnerId { get; set; }

        [JsonProperty(PropertyName = "priority")]
        public int? Priority { get; set; } // Probably could be a byte but I need to make sure it won't crash

        [JsonProperty(PropertyName = "requester_id")]
        public long? RequesterId { get; set; }

        [JsonProperty(PropertyName = "responder_id")]
        public long? ResponderId { get; set; }

        [JsonProperty(PropertyName = "source")]
        public int? Source { get; set; }

        [JsonProperty(PropertyName = "spam")]
        public bool Spam { get; set; }

        [JsonProperty(PropertyName = "status")]
        public int? Status { get; set; }

        [JsonProperty(PropertyName = "subject")]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = "ticket_type")]
        public string TicketType { get; set; }

        [JsonProperty(PropertyName = "to_email")]
        public string ToEmail { get; set; }

        [JsonProperty(PropertyName = "trained")]
        public bool Trained { get; set; }

        [JsonProperty(PropertyName = "updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty(PropertyName = "urgent")]
        public bool Urgent { get; set; }

        [JsonProperty(PropertyName = "status_name")]
        public string StatusName { get; set; }

        [JsonProperty(PropertyName = "requester_status_name")]
        public string RequesterStatusName { get; set; }

        [JsonProperty(PropertyName = "priority_name")]
        public string PriorityName { get; set; }

        [JsonProperty(PropertyName = "source_name")]
        public string SourceName { get; set; }

        [JsonProperty(PropertyName = "requester_name")]
        public string RequesterName { get; set; }

        [JsonProperty(PropertyName = "responder_name")]
        public string ResponderName { get; set; }
    }
}
