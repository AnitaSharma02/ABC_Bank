using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class BookingStatusResponseForDomestic
    {
        public bool Status { get; set; }
        public BookedDetails Detail { get; set; }
    }
    public class BookedDetails
    {
        public string OutboundState { get; set; }
        public string OutboundStatus { get; set; }
        public string FlightId { get; set; }
        public string Reference { get; set; }
        public string ResponseId { get; set; }
        public DetailsOfBookedFlight Outbound { get; set; }
        public string InboundState { get; set; }
        public string InboundStatus { get; set; }
        public string InboundFlightId { get; set; }
        public DetailsOfBookedFlight Inbound { get; set; }
        public List<PassengersDetails> Passengers { get; set; }
    }
    public class DetailsOfBookedFlight
    {
        public string Airline { get; set; }
        public string AirlineName { get; set; }
        public string Pnrno { get; set; }
        public string Flightno { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartureTime { get; set; }
        public string FlightClassCode { get; set; }
        public string Currency { get; set; }
        public decimal FareTotal { get; set; } = 0;
        public string ReportingTime { get; set; } = string.Empty;
        public string InboundReportingTime { get; set; } = string.Empty;
    }

    public class PassengersDetails
    {
       public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Title { get; set; }
        public string PassengerType { get; set; }
        public string Gender { get; set; }
        public string TicketNo { get; set; }
        public string Barcode { get; set; } = string.Empty;
        public string InboundTicketNo { get; set; }
        public string InboundBarcode { get; set; } = string.Empty;
    }
}
