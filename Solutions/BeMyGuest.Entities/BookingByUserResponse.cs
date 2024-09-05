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
    public class BookingByUserResponse
    {
        [DataMember]
        public int success { get; set; }
        [DataMember]
        public string memberId { get; set; }
        [DataMember]
        public List<BookingByUserResponseData> data { get; set; }
        [DataMember]
        public ProductMeta meta { get; set; }
    }
}
