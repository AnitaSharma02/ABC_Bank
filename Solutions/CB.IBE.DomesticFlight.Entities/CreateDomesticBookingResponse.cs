using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class CreateDomesticBookingResponse
    {
        public bool Status { get; set; }=false;
        public string State { get; set; }
        public string FlightDate { get; set; }
        public string ReturnDate { get; set; } = null;
        public string SectorFrom { get; set; }
        public string SectorTo { get; set;}
        public string TripType { get;set; }
        public int CreditsAvailable { get;set; }
        public int CreditsConsumed { get;set; }
        public int Commission { get;set; }
        public string Message { get; set; }
        public BookedFlightDetails Outbound { get; set; }
        public BookedFlightDetails Inbound { get; set; }
        public List<Passengers> Passengers { get; set; }
        public List<int> LogIds { get; set; }
    }
    public class BookedFlightDetails
    {
        public string Airline { get; set; }
        public string AirlineName { get; set; }
        public string Pnrno { get; set; }
        public string Flightno { get;set; } 
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set;}
        public string FlightClassCode { get; set; }
        public string Currency { get; set;}
        public string FareTotal { get; set; }
        public string ReportingTime { get; set; } = string.Empty;
        public string InboundReportingTime { get; set;}=string.Empty;
    }

    public class Passengers 
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string PassengerType { get; set; }
        public string Gender { get; set; }
        public string TicketNo { get;set; }
        public string Barcode { get;set; }=string.Empty;
        public string InboundTicketNo { get; set; }
        public string InboundBarcode { get; set; }=string.Empty;

    }
}
