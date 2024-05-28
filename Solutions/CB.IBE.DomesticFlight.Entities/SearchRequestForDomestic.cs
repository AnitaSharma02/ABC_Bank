using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class SearchRequestForDomestic
    {
        public string ReferenceId { get; set; }
        public string MemberId { get; set; }
        public string Token { get; set; }
        public int Adults { get; set; }
        public int Childrens { get; set; } = 0;
        public string DepartureDate { get; set; }
        public string DestinationLocation { get; set; } 
        public string IPAddress { get; set; } = string.Empty;
        public string ReturnDate  { get; set; }= null;
        public bool IsReturn { get; set; } = false;
        public string OriginLocation { get; set; }
        public float PointRate { get; set; } = 0.0f;
        public string DeptCity { get; set; }
        public string ArrivalCity { get; set; } = string.Empty;
    }
}

