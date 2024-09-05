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
    public class BookingByUserRequest
    {
        [DataMember]
        public string memberId { get; set; }
        [DataMember]
        public int? page { get; set; }
        [DataMember]
        public int? per_page { get; set; }
    }
}
