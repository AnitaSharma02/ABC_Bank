using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiInsurance.Entities
{
    public class InsurancePaymentRequestResponse
    {
        public PaymentResponse results { get; set; }
    }

    public class PaymentResponse
    {
        public bool Status { get; set; }
        public string State { get; set; }
        public string ReferenceId { get; set; } = string.Empty;
        public string Message { get; set; }
        public string Detail { get; set; }
        public decimal CreditsConsumed { get; set; } = 0;
        public decimal CreditsAvailable { get; set; } = 0;
        public int Id { get; set; } = 0;
        public string ErrorCode { get; set; } = string.Empty;
    }

    public class PaymentRequestParameters
    {
        public string ServiceCode = string.Empty;
        public decimal Amount = 0;
        public int SessionId = 0;
        public string RequestId= string.Empty;
        public string PolicyNo = string.Empty;
        public string TransactionId = string.Empty;
        public string MembershipReference = string.Empty;
        public string CustomerName = string.Empty;
    }
}
