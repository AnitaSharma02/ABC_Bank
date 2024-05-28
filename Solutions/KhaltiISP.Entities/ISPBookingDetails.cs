using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiISP.Entities
{
    public class ISPBookingDetails
    {
        public List<ISPBookingResponse> results { get; set; }
    }
    public class ISPBookingResponse
    {
        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public string PolicyNo { get; set; }
        public string RequestId { get; set; }
        public string ReferenceId { get; set; }
        public bool PaymentStatus { get; set; }
        public float Amount { get; set; }
        public int PaymentId { get; set; }
        public string InvoiceNo { get; set; }
        public string TransactionId { get; set; }
        public string NextDueDate { get; set; }
        public string PaymentDate { get; set; }
    }
}
