using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{
    public class PaymentGatewayDetails
    {
        #region Private Variables

        public int Id { get; set; }
        public string TransactionType { get; set; }
        public int RequestId { get; set; }
        public string APIKey { get; set; }
        public int Points { get; set; }
        public int Source { get; set; }
        public decimal SourceAmount { get; set; }
        public string SourceCurrency { get; set; }
        public string AdditionalDetails1 { get; set; }
        public string AdditionalDetails2 { get; set; }
        public string AdditionalDetails3 { get; set; }
        public string AdditionalDetails4 { get; set; }
        public string AdditionalDetails5 { get; set; }
        public string TransactionID { get; set; }
        public DateTime TransactionDateTime { get; set; }
        public DateTime ProcessingDateTime { get; set; }
        public string Description { get; set; }
        public string ExternalReference { get; set; }
        public string TxnToken { get; set; }
        public string TxnReference { get; set; }
        public int Status { get; set; }
        public string IpAddress { get; set; }
        public string LoyaltyId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int ReconciledStatus { get; set; }

        #endregion
    }
}
