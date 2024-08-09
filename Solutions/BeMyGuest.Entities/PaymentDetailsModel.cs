using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    public class PaymentDetailsModel
    {
        [DataMember]
        public BookingRequest BookingRequest { get; set; }
        [DataMember]
        public ProductInfoResponse ProductInfoResponse { get; set; }
    }
}
