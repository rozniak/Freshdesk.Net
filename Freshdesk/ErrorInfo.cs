using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Freshdesk
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ErrorInfo
    {
        [JsonProperty(PropertyName = "no_company")]
        public bool NoCompany { get; set; }
    }
}
