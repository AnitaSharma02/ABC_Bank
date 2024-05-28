using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class BookingStatusRequestForDomestic
    {
        public string Token { get; set; }
        public string Reference { get; set; }
        public float PointRate { get; set; } = 0.0f;
    }
}
