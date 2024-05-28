using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiInsurance.Entities
{
    public class InsuranceUserDetailsResponse
    {
        public UserDetails results { get; set; }
    }
    public class UserDetails
    {
        public bool Status { get; set; }
        public string PayMode { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public string ProformaNo { get; set; } = string.Empty;
        public string CustomerName { get; set; } = null;
        public string CustomerId { get; set; } = string.Empty;
        public string PolicyNo { get; set; } = null;
        public string InstallmentNo { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string InvoiceNo { get; set; } = string.Empty;
        public string PolicyStatus { get; set; } = string.Empty;
        public string PlanCode { get; set; } = string.Empty;
        public string DueDate { get; set; } = null;
        public string NextDueDate { get; set; } = string.Empty;
        public string CurrentDueDate { get; set; } = string.Empty;
        public string PaymentDate { get; set; } = string.Empty;
        public string MaturityDate { get; set; } = string.Empty;
        public string Term { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        public decimal PremiumAmount { get; set; } = 0;
        public decimal RebateAmount { get; set; } = 0;
        public decimal FineAmount { get; set; } = 0;
        public decimal AdjustmentAmount { get; set; } = 0;
        public decimal TP_Premium { get; set; } = 0;
        public decimal SumInsured { get; set; } = 0;
        public string ErrorCode { get; set; } = null;
        public string Message { get; set; } = string.Empty;
        public int SessionId { get; set; } = 0;
        public string Key { get; set; } = string.Empty;
        public float PointRate { get; set; } = 0.0f;
    }
    public class UserDetailsParameters 
    {
        public int ServiceCode = 0;
        public string PolicyNo = string.Empty;
        public string DOB = string.Empty;
        public string RequestId=string.Empty;
        public string Username = string.Empty;
        public string CustomerId = string.Empty;
        public string MembershipReference = string.Empty;
    }
}
