using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class BookingOption
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string label { get; set; }
        [DataMember]
        public string value { get; set; }
        [DataMember]
        public int price { get; set; }
    }
}
