using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class Paths
    {
        [DataMember]
        public string noriginal { get; set; }
        [DataMember]
        [JsonProperty("75x50")]
        public string _75x50 { get; set; }
        [DataMember]
        [JsonProperty("175x112")]
        public string _175x112 { get; set; }
        [DataMember]
        [JsonProperty("680x325")]
        public string _680x325 { get; set; }
        [DataMember]
        [JsonProperty("1280x720")]
        public string _1280x720 { get; set; }
        [DataMember]
        [JsonProperty("1920x1080")]
        public string _1920x1080 { get; set; }
        [DataMember]
        [JsonProperty("2048x1536")]
        public string _2048x1536 { get; set; }
    }
}