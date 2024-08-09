using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class BookingData
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string partnerReference { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string productTypeTitle { get; set; }
        [DataMember]
        public string productTypeTitleTranslated { get; set; }
        [DataMember]
        public string productTypeUuid { get; set; }
        [DataMember]
        public string currencyCode { get; set; }
        [DataMember]
        public string currencyUuid { get; set; }
        [DataMember]
        public decimal totalAmount { get; set; } = 0;
        [DataMember]
        public decimal grandTotalAmount { get; set; } = 0;
        [DataMember]
        public List<AmountBreakdown> amountBreakdown { get; set; }
        [DataMember]
        public string arrivalDate { get; set; }
        [DataMember]
        public string timeSlot { get; set; }
        [DataMember]
        public string createdAt { get; set; }
        [DataMember]
        public string updatedAt { get; set; }
        [DataMember]
        public string salutation { get; set; }
        [DataMember]
        public string firstName { get; set; }
        [DataMember]
        public string lastName { get; set; }
        [DataMember]
        public string email { get; set; }
        [DataMember]
        public string phone { get; set; }
        [DataMember]
        public int adults { get; set; }
        [DataMember]
        public int children { get; set; }
        [DataMember]
        public int seniors { get; set; }
        [DataMember]
        public List<TicketType> ticketTypes { get; set; }
        [DataMember]
        public List<BookingOption> options { get; set; }
        [DataMember]
        public string completedAt { get; set; }
        [DataMember]
        public string cancellationRequestAt { get; set; }
        [DataMember]
        public string cancellationRequestStatus { get; set; }
        [DataMember]
        public string cancellationStatus { get; set; }
        [DataMember]
        public string refundDate { get; set; }
        [DataMember]
        public decimal? refundAmount { get; set; }
        [DataMember]
        public string refundTransaction { get; set; }
        [DataMember]
        public decimal convertedAmount { get; set; }
        [DataMember]
        public string convertedCurrency { get; set; }
        [DataMember]
        public string cancellationPolicySummary { get; set; }
        [DataMember]
        public string prodtitle { get; set; }
        [DataMember]
        public string prodavailaddress { get; set; }
        [DataMember]
        public string meetingTime { get; set; }
        [DataMember]
        public string meetingAddress { get; set; }
        [DataMember]
        public string meetingLocation { get; set; }
    }
}
