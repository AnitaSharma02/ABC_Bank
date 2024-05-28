using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class CreateDomesticItineraryResponse
    {
        public bool Status { get; set; } = false;
        public DateTime Ttl { get; set; }
        public string FlightId { get; set; }
        public string InboundFlightId { get; set;}
        public int Commission { get; set; } = 0;
        public int AdultCommission { get; set; } = 0;
        public int ChildCommission { get; set; } = 0;
        public int InboundCommission { get; set; } = 0;
        public int InboundAdultCommission { get;set; } = 0;
        public int InboundChildCommission { get;set;} = 0;

    }
}
