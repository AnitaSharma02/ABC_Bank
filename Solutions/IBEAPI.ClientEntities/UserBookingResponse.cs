using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class UserBookingDetails
    {
        public int Id { get; set; }
        public string Title_ID { get; set; }
        public string Member_ID { get; set; }
        public string Member_Name { get; set; }
        public string Member_Email { get; set; }
        public string Member_Phone { get; set; }
        public string Payment_Method { get; set; }
        public string Rate_Referece { get; set; }
        public string Lang { get; set; }
        public string Broker_Reference { get; set; }
        public string Flight_Number { get; set; }
        public string Reserve_Number { get; set; }
        public string Access_Token { get; set; }
        public double Payment_Amount { get; set; }
        public string Payment_Currency { get; set; }
        public DateTime createDateTime { get; set; }
        public bool IsActive { get; set; }
        public int Version_Number { get; set; }
        public string Reference_Unique_Id { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public string LastTransactionStatus { get; set; }
        public string Source { get; set; }
        public int DurationDays { get; set; }
        public string ResidenceCountry { get; set; }
        public DateTime pickUpDateTime { get; set; }
        public DateTime dropOffDateTime { get; set; }
        public int driverAge { get; set; }
        public string Vehicle_UniqueRef { get; set; }
        public string Vehicle_name { get; set; }
        public string Vehicle_acrissCode { get; set; }
        public string Vehicle_type { get; set; }
        public string Vehicle_transmission { get; set; }
        public bool Vehicle_airco { get; set; }
        public string Vehicle_doors { get; set; }
        public string Vehicle_fuelType { get; set; }
        public bool Vehicle_modelGuaranteed { get; set; }
        public string Vehicle_supplierCode { get; set; }
        public int Vehicle_seats { get; set; }
        public int Vehicle_smallSuitcases { get; set; }
        public int Vehicle_bigSuitcases { get; set; }
        public bool Vehicle_builtInGps { get; set; }
        public string Vehicle_transmissionText { get; set; }
        public string Vehicle_fuelTypeText { get; set; }
        public string pickUpBranchLine { get; set; }
        public string dropOffBranchLine { get; set; }
    }

    public class UserBookingResponse
    {
        public int success { get; set; }
        public List<UserBookingDetails> data { get; set; }
    }
}
