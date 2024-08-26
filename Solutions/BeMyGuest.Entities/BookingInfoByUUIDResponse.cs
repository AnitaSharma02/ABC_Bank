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
    public class BookingInfoByUUIDResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public BookingInfoByUUIDResponseData data { get; set; }
    }
}
