using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace KhaltiISP.Entities
{
    public class ISPUserDetailsResponse
    {
        public Results results { get; set; }
    }
    public class Results
    {
        public CustomerDetails CustomerDetails { get; set; }
        public CurrentPlan CurrentPlan { get; set; }
        public List<Packages> Packages { get; set; }
        public string Plans { get; set; } = null;
        public decimal Amount { get; set; } = 0;
        public int SessionId { get; set; }
        public bool Status { get; set; } = false;
        public string Message { get; set; } = string.Empty;
        public string ErrorCode { get; set; } = string.Empty;
        public string Key { get; set; }
        public float PointRate { get; set; } = 0.0f;
    }

    public class CustomerDetails
    {
        public string Username { get; set; } = null;
        public string CustomerName { get; set; }
        public string CustomerId { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Address { get; set; } = null;
        public string Branch { get; set; } = null;
        public string InvoiceNo { get; set; }
        public string Status { get; set; } = null;
    }
    public class CurrentPlan
    {
        public string PlanName { get; set; } = null;
        public string PlanType { get; set; } = null;
        public int PlanId { get; set; } = 0;
        public bool AcceptAdvancedPayment { get; set; } = false;
        public bool isDue { get; set; } = false;
        public decimal Amount { get; set; } = 0;
        public int DaysRemaining { get; set; } = 0;
        public decimal DueAmount { get; set; } = 0;
        public string Message { get; set; } = null;
        public decimal PreviousBalance { get; set; } = 0;
        public string EndDate { get; set; } = null;
        public string Status { get; set; } = null;
        public List<Details> Details { get; set; } = null;
    }
    public class Packages
    {
        public int Id { get; set; }
        public string Package { get; set; }
        public string PackageId { get; set; }
        public string PackageSubId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; } = null;
        public bool Current { get; set; } = false;
        public List<Details> Details { get; set; } = null;
    }
    public class Details
    {
        public int Id { get; set; }
        public string Particular { get; set; }
        public string Duration { get; set; } = null;
        public string DurationCode { get; set; } = null;
        public string PackageSubId { get; set; } = null;
        public decimal Amount { get; set; } = 0;
    }
}
