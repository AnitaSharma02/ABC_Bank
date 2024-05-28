using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class CreateDomesticItineraryRequest
    {
        public string Token { get; set; }
        public string MemberId { get; set; }
        public string FlightId { get; set; }
        public int BookingId { get; set; }
        public string ReturnFlightId { get; set; } = string.Empty;
        public float PointRate { get; set; } = 0.0f;
    }
}
