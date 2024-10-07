using CB.IBE.Platform.Entities;
using System;
using System.Runtime.Serialization;

namespace IBEAPI.ClientEntities
{
    public class AirSearchRequest
    {
        public int Adults { get; set; }
        public int Childrens { get; set; }
        public int Infants { get; set; }
        public string AirlinePrefCode { get; set; }
        public string Cabin { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string OriginLocation { get; set; }
        public string DestinationLocation { get; set; }
        public string IPAddress { get; set; }
        public bool IsReturn { get; set; }
        public string MemberId { get; set; }
        public string ResultCount { get; set; }
        public double PointRate { get; set; }
        public string DepCountryName { get; set; }
        public string ArrCountryName { get; set; }
        private AirField mobjDepCode = new AirField();
        private AirField mobjArrCode = new AirField();

        [DataMember]
        public AirField DepCode
        {
            get { return mobjDepCode; }
            set { mobjDepCode = value; }
        }

        [DataMember]
        public AirField ArrCode
        {
            get { return mobjArrCode; }
            set { mobjArrCode = value; }
        }
    }
}