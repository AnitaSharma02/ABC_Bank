using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class CdwExcess
    {
        public Payment payment { get; set; }
        public Local local { get; set; }
    }

    public class Content
    {
        public string text { get; set; }
    }

    public class Coverage
    {
        public string code { get; set; }
        public string name { get; set; }
        public bool mandatory { get; set; }
        public bool includedInVehiclePrice { get; set; }
        public RentalPrice rentalPrice { get; set; }
        public ExcessAmount excessAmount { get; set; }
        public string paymentType { get; set; }
    }

    public class Custom
    {
        public string title { get; set; }
        public List<Section> sections { get; set; }
    }

    public class RateData
    {
        public string lang { get; set; }
        public bool error { get; set; }
        public string paymentCurrency { get; set; }
        public string displayCurrency { get; set; }
        public Vehicle vehicle { get; set; }
        public string pickUpBranchId { get; set; }
        public string dropOffBranchId { get; set; }
        public string source { get; set; }
        public DateTime pickUpDateTime { get; set; }
        public DateTime dropOffDateTime { get; set; }
        public Package package { get; set; }
        public List<Branch> branches { get; set; }
        public Terms terms { get; set; }
        public ResidenceCountry residenceCountry { get; set; }
    }

    public class ExcessAmount
    {
        public Payment payment { get; set; }
        public Local local { get; set; }
    }

    public class Full
    {
        public Custom custom { get; set; }
    }

    public class RateLocation
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

    public class Net
    {
        public Payment payment { get; set; }
        public Local local { get; set; }
    }

    public class RatePackage
    {
        public int id { get; set; }
        public string paymentType { get; set; }
        public FuelPolicy fuelPolicy { get; set; }
        public int contractId { get; set; }
        public bool onRequest { get; set; }
        public string rateReference { get; set; }
        public string providerReference { get; set; }
        public int providerId { get; set; }
        public string rateSource { get; set; }
        public Payments payments { get; set; }
        public VehiclePrice vehiclePrice { get; set; }
        public List<Extra> extras { get; set; }
        public List<Fee> fees { get; set; }
        public List<Coverage> coverages { get; set; }
        public List<Inclusion> inclusions { get; set; }
        public Deposit deposit { get; set; }
        public CdwExcess cdwExcess { get; set; }
        public string deepLink { get; set; }
        public int priceListId { get; set; }
        public bool unlimitedMileage { get; set; }
        public bool hasCDW { get; set; }
    }

    public class RateResponse
    {
        public int success { get; set; }
        public RateData data { get; set; }
    }

    public class Section
    {
        public string title { get; set; }
        public string content { get; set; }
    }

    public class Short
    {
        public string code { get; set; }
        public string name { get; set; }
        public List<Content> contents { get; set; }
    }

    public class Terms
    {
        public Full full { get; set; }
        public List<Short> @short { get; set; }
    }
    public class VehiclePrice
    {
        public string totalPayment { get; set; }
        public Total total { get; set; }
        public Net net { get; set; }
        public int pricingRuleId { get; set; }
        public string rateType { get; set; }
    }


}


