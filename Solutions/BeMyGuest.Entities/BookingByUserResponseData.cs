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
    public class BookingByUserResponseData
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string code { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string convertedCurrency { get; set; }
        [DataMember]
        public decimal totalAmount { get; set; } = 0;
        [DataMember]
        public decimal grandTotalAmount { get; set; } = 0;
        [DataMember]
        public string arrivalDate { get; set; }
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
        public decimal? refundAmount { get; set; } = 0;
        [DataMember]
        public decimal refundTransaction { get; set; } = 0;
        [DataMember]
        public string prodtitle { get; set; }
        [DataMember]
        public string productTypeTitle { get; set; }
        [DataMember]
        public string bookingDate { get; set; }
    }
}
