using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class SearchResponseForDomestic
    {
        public bool Status { get; set; } = false;

        public int BookingId { get; set; } = 0;

        public List<FlightDetails> Outbound { get; set; }

        public List<FlightDetails> Inbound { get; set; } =null;

    }
    public class FlightDetails
    {
        public string Airline { get; set; }
        public string AirlineName { get; set; }
        public string AirlineLogo { get; set; }
        public string FlightDate { get; set; }
        public string FlightNo { get; set; }
        public string Departure { get; set; }
        public string DepartureTime { get; set; }
        public string Arrival { get;set; }
        public string ArrivalTime { get; set; }
        public string AircraftType { get; set; }
        public int Adult { get; set; }
        public string Child { get;set; }
        public string Infant { get; set;}
        public string FlightClassCode { get; set; }
        public string Currency { get; set; }
        public decimal AdultFare { get; set; }
        public decimal ChildFare { get; set;}
        public decimal InfantFare { get; set; }
        public decimal ResFare { get;set; }
        public decimal FuelSurcharge { get;set; }
        public decimal Tax { get; set; } 
        public bool Refundable { get; set; }
        public string FreeBaggage { get; set; }
        public decimal FareTotal { get; set; }
        public decimal Commission { get; set; }
        public string FlightId { get; set; }
        public decimal AgencyCommission { get; set; }
        public decimal ChildCommission { get; set; }
        public decimal AdultCommission { get; set; }

    }

    public class FinalFlightResult
    {
        public string Departure_AirlineName { get; set; }
        public string Departure_AirlineLogo { get; set; }
        public string Departure_FlightDate { get; set; }
        public string Departure_FlightNo { get; set; }
        public string Departure_Departure { get; set; }
        public string Departure_DepartureTime { get; set; }
        public string Departure_Arrival { get; set; }
        public string Departure_ArrivalTime { get; set; }
        public string Departure_FreeBaggage { get; set; }
        public decimal Departure_FareTotal { get; set; }
        public string Departure_FlightId { get; set; }
        public string Departure_AircraftType { get; set; } 
        public string Return_AirlineName { get; set; } = string.Empty;
        public string Return_AirlineLogo { get; set; } = string.Empty;
        public string Return_FlightDate { get; set; } = string.Empty;
        public string Return_FlightNo { get; set; } = string.Empty;
        public string Return_Departure { get; set; } = string.Empty;
        public string Return_DepartureTime { get; set; } = string.Empty;
        public string Return_Arrival { get; set; } = string.Empty;
        public string Return_ArrivalTime { get; set; } = string.Empty;
        public string Return_FreeBaggage { get; set; } = string.Empty;
        public decimal Return_FareTotal { get; set; } = 0;
        public string Return_FlightId { get; set; } = string.Empty;
        public string Return_AircraftType { get; set; } = string.Empty;
        public decimal FareTotal { get; set; } = 0;
        public bool IsReturn { get; set; } = false;
    }

    public class SelectedFinalDomesticFlights
    {
        public List<FlightDetails> SelectedFlights { get; set; } = null;
    }
}
