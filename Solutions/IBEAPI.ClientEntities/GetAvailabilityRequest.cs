using Framework.Integrations.Hotels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class DropOff
    {
        public string dateTime { get; set; }
        public string date { get; set; }
        public string Time { get; set; }
        public Location location { get; set; }
    }

    public class Location
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class PickUp
    {
        public string dateTime { get; set; }

        public string date { get; set; }
        public string Time { get; set; }
        public Location location { get; set; }
    }

    public class ResidenceCountry
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }

    public class GetAvailabilityRequest
    {
        public string lang { get; set; }
        public string source { get; set; }
        public PickUp pickUp { get; set; }
        public DropOff dropOff { get; set; }
        public int driverAge { get; set; }
        public ResidenceCountry residenceCountry { get; set; }
        public string displayCurrency { get; set; }
        public string vehicleType { get; set; }
        public int vehicleAtId { get; set; }
        public int supplierId { get; set; }
    }
    public class AvailabilityResponse
    {
        public int success { get; set; }
        public AvailabilityData data { get; set; }
    }
    public class GeoPoint
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
    }
    public class Branch
    {
        public string id { get; set; }
        public Supplier supplier { get; set; }
        public string supplierCode { get; set; }
        public GeoPoint geoPoint { get; set; }
        public string openHoursText { get; set; }
        public string phoneNumber { get; set; }
        public string email { get; set; }
        public string requireFlightNo { get; set; }
        public VehicleAt vehicleAt { get; set; }
        public bool isMeetAndGreet { get; set; }
        public Location location { get; set; }
        public AddressData addressData { get; set; }
        public string address { get; set; }
        public List<Mapping> mappings { get; set; }
        public bool isClosed { get; set; }
    }
    public class AvailabilityData
    {
        public bool error { get; set; }
        public string paymentCurrency { get; set; }
        public List<Rate> rates { get; set; }
        public List<Branch> branches { get; set; }
        public List<Warning> warnings { get; set; }
        public string queryId { get; set; }
    }
    public class VehicleAt
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    public class Mapping
    {
        public int providerId { get; set; }
        public string providerReference { get; set; }
    }
    public class AddressData
    {
        public string line1 { get; set; }
        public string line3 { get; set; }
        public string postalCode { get; set; }
    }
    public class Details
    {
        public int providerType { get; set; }
    }
    public class Supplier
    {
        public int id { get; set; }
        public string name { get; set; }
        public string logoSvgUrl { get; set; }
    }
    public class Warning
    {
        public string code { get; set; }
        public string message { get; set; }
        public Details details { get; set; }
    }
}
