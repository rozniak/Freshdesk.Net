using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk
{
    [JsonObject(MemberSerialization.OptIn)]
    public class CreateTimeInfo
    {
        [JsonProperty(PropertyName = "note")]
        public string Note { get; set; }

        [JsonProperty(PropertyName = "hhmm")]
        public double Time { get; set; }

        [JsonProperty(PropertyName = "user_id")]
        public long UserId { get; set; }
    }
}
