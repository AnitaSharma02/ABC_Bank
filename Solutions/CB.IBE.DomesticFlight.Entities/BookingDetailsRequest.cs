using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class BookingDetailsRequest
    {
        public string Token { get; set; }
        public string FlightId { get; set; }
        public int BookingId { get; set; }
        public string ReturnFlightId { get; set; } = string.Empty;
        public string MemberId { get; set; }
        public float PointRate { get; set; } = 0.0f;
        public PassengersContactInfo PassengersContactInfo { get; set; }
        public List<PassengerDetailsForDomestic> PassengersInfo { get; set; }
    }

    public class PassengerDetailsForDomestic
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
    }
    public class PassengersContactInfo
    {
        public string ContactName { get; set;}
        public string ContactPhone { get; set;}
        public string ContactEmail { get; set;}
    }
}
