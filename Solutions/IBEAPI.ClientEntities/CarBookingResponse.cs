using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class BookingResponseData
    {
        public string reference_id { get; set; }
    }

    public class CarBookingResponse
    {
        public int success { get; set; }
        public BookingResponseData data { get; set; }
    }
}
