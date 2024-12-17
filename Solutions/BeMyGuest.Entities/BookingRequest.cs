using System;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class BookingRequest
    {
        [DataMember]
        public string message { get; set; } = string.Empty;
        [DataMember]
        public string productUuid { get; set; } = string.Empty;
        [DataMember]
        public string productTypeUuid { get; set; } = string.Empty;
        [DataMember]
        public Customer customer { get; set; } = new Customer();
        [DataMember]
        public int adults { get; set; } = 0;
        [DataMember]
        public int children { get; set; } = 0;
        [DataMember]
        public int seniors { get; set; } = 0;
        [DataMember]
        public string timeSlotUuid { get; set; } = string.Empty;
        [DataMember]
        public string arrivalDate { get; set; } = string.Empty;
        [DataMember]
        public ExperiencesBookingRequestOptions options { get; set; } = new ExperiencesBookingRequestOptions();
        [DataMember]
        public string memberId { get; set; } = string.Empty;
        [DataMember]
        public decimal totalAmount { get; set; } = 0;
        [DataMember]
        public string currencyCode { get; set; } = string.Empty;

        [DataMember]
        public string titleName { get; set; }=string.Empty;
        [DataMember]
        public string pointConvrtRate { get; set; }
    }
}