using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiISP.Entities
{
    public class ISPPaymentResponse
    {
        public PaymentResponse results { get; set; }
    }
    public class PaymentResponse
    {
       public  bool Status { get; set; } = false;
        public string State { get; set; }
        public string NextDueDate { get; set; } = string.Empty;
        public string ReferenceId { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public decimal CreditsConsumed { get; set; } = 0;
        public decimal CreditsAvailable { get; set; } = 0;
        public int Id { get; set; } = 0;
        public string InvoiceNo { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string ReceiptNumber { get; set; } = string.Empty;
        public string ReceiptDate { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
    }
}
