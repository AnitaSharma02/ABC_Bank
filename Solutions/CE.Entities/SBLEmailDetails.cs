using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CE.Entities
{
    public class NICEmailDetails
    {
        [JsonProperty("event_name")]
        public string CE_Event = string.Empty;
        public string relation_reference { get; set; }
        public int program_id { get; set; }
        public string to_email { get; set; }
        public string full_name { get; set; }
        public string otp { get; set; }
        public string to_mobile { get; set; }
        public string redemption_type { get; set; }
    }
}
