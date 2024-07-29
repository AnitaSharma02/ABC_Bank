using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace IBEAPI.ClientEntities
{

    public class Deposit
    {
        public Payment payment { get; set; }
        public Local local { get; set; }
    }

    public class Display
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class DriverAge
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class EstimatedTotal
    {
        public Vehicle vehicle { get; set; }
        public Fees fees { get; set; }
        public Total total { get; set; }
    }
    [DataContract]
    public class Extra
    {
        [JsonProperty("code", NullValueHandling = NullValueHandling.Ignore)]
        public string code { get; set; }
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string name { get; set; }
        [JsonProperty("description", NullValueHandling = NullValueHandling.Ignore)]
        public string description { get; set; }
        [JsonProperty("maxQuantity", NullValueHandling = NullValueHandling.Ignore)]
        public int maxQuantity { get; set; }
        [JsonProperty("quantity", NullValueHandling = NullValueHandling.Ignore)]
        public int quantity { get; set; }
        [JsonProperty("includedInVehiclePrice", NullValueHandling = NullValueHandling.Ignore)]
        public bool includedInVehiclePrice { get; set; }
        [JsonProperty("mandatory", NullValueHandling = NullValueHandling.Ignore)]
        public bool mandatory { get; set; }
        [JsonProperty("rentalPrice", NullValueHandling = NullValueHandling.Ignore)]
        public RentalPrice rentalPrice { get; set; }
        [JsonProperty("paymentType", NullValueHandling = NullValueHandling.Ignore)]
        public string paymentType { get; set; }
        [JsonProperty("pricingRuleId", NullValueHandling = NullValueHandling.Ignore)]
        public int pricingRuleId { get; set; }
        [JsonProperty("productId", NullValueHandling = NullValueHandling.Ignore)]
        public int productId { get; set; }
        [JsonProperty("groupType", NullValueHandling = NullValueHandling.Ignore)]
        public string groupType { get; set; }
    }

    public class Fee
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool mandatory { get; set; }
        public bool includedInVehiclePrice { get; set; }
        public RentalPrice rentalPrice { get; set; }
        public string paymentType { get; set; }
        public Payment payment { get; set; }
        public Local local { get; set; }
        public Display display { get; set; }
    }

    public class FuelPolicy
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Images
    {
        public string size { get; set; }
        public string url { get; set; }
    }

    public class Inclusion
    {
        public string code { get; set; }
        public string name { get; set; }
    }

    public class Local
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class AvaibilityLocation
    {
        public Coords coords { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public bool isAirport { get; set; }
        public bool isRailway { get; set; }
        public bool isPort { get; set; }
        public bool isBus { get; set; }
        public City city { get; set; }
        public Country country { get; set; }
    }
    public class MileagePolicy
    {
        public int mileageIncluded { get; set; }
        public string distanceUnit { get; set; }
        public string excessMileageRate { get; set; }
    }

    public class Fees
    {
        public Payment payment { get; set; }
        public Display display { get; set; }
    }

    public class Package
    {
        public int id { get; set; }
        public string paymentType { get; set; }
        public FuelPolicy fuelPolicy { get; set; }
        public int contractId { get; set; }
        public bool onRequest { get; set; }
        public string rateReference { get; set; }
        public Payments payments { get; set; }
        public VehiclePrice vehiclePrice { get; set; }
        public List<Extra> extras { get; set; }
        public List<Fee> fees { get; set; }
        public List<object> coverages { get; set; }
        public List<Inclusion> inclusions { get; set; }
        public Deposit deposit { get; set; }
        public string deepLink { get; set; }
        public int priceListId { get; set; }
        public bool unlimitedMileage { get; set; }
        public bool hasCDW { get; set; }
        public MileagePolicy mileagePolicy { get; set; }
    }

    public class PayLocal
    {
        public Vehicle vehicle { get; set; }
        public Fees fees { get; set; }
        public Total total { get; set; }
    }

    public class Payment
    {
        public double amount { get; set; }
        public string currency { get; set; }
    }

    public class Payments
    {
        public EstimatedTotal estimatedTotal { get; set; }
        public PayLocal payLocal { get; set; }
        public PayNow payNow { get; set; }
    }

    public class PayNow
    {
        public Vehicle vehicle { get; set; }
        public Fees fees { get; set; }
        public Total total { get; set; }
    }

    public class Rate
    {
        public Vehicle vehicle { get; set; }
        public string pickUpBranchId { get; set; }
        public string dropOffBranchId { get; set; }
        public DriverAge driverAge { get; set; }
        public List<Package> packages { get; set; }
        public string vehicleType { get; set; }
    }

    public class RentalPrice
    {
        public Payment payment { get; set; }
        public Display display { get; set; }
        public Local local { get; set; }
    }

    public class Total
    {
        public Payment payment { get; set; }
        public Local local { get; set; }
        public Display display { get; set; }
    }

    public class Vehicle
    {
        public string uniqueRef { get; set; }
        public string name { get; set; }
        public string acrissCode { get; set; }
        public string type { get; set; }
        public string transmission { get; set; }
        public bool airco { get; set; }
        public string doors { get; set; }
        public string fuelType { get; set; }
        public bool modelGuaranteed { get; set; }
        public string supplierCode { get; set; }
        public string seats { get; set; }
        public int smallSuitcases { get; set; }
        public int bigSuitcases { get; set; }
        public bool builtInGps { get; set; }
        public string transmissionText { get; set; }
        public string fuelTypeText { get; set; }
        public List<Images> images { get; set; }
        public string imageTypeName { get; set; }
        public string imageTypeUrl { get; set; }
        public Payment payment { get; set; }
        public Local local { get; set; }
        public Display display { get; set; }

    }
}
