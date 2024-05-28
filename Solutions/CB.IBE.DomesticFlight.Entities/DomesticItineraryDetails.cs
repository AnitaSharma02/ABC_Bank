using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class DomesticItineraryDetails
    {
        public string FlightDate { get; set; }
        public string ReturnDate { get; set; }  
        public string SectorFrom { get; set; }
        public string SectorTo { get; set; }
        public string CreditsConsumed { get; set; }
        public string InboundPNR { get; set; } = null;
        public string OutboundPNR { get; set; }
        public List<int> LogIds { get; set; }
    }
}
