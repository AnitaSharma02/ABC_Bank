using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class CustomerDetails
    {
        public int titleId { get; set; }
        public string memberId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
    }

    public class CarBookingPayment
    {
        public string method { get; set; }
    }

    public class CarBookingRequest
    {
        public CustomerDetails customer { get; set; }
        public CarBookingPayment payment { get; set; }
        public string rateReference { get; set; }
        public string lang { get; set; }
        public string flightNumber { get; set; }
        public List<object> extras { get; set; }
        public string brokerReference { get; set; }
        public string pointrate { get; set; }
    }
}
