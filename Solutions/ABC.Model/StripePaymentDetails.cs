using CB.IBE.Platform.Masters.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Model
{
    public class StripePaymentDetails
    {
        public string TotalProductAmount { get; set; }
        public string Currency { get; set; }
        public string ProductName { get; set; }
        public string RedemptionType { get; set; }
        public string CustomerEmail { get; set; }
        public string ClientReferenceId { get; set; }
        public string ReqRedeemPoint { get; set; }
        public string ReqRedeemPointAmount { get; set; }
        public string ReqRedeemAmount { get; set; }
        public string RedeemMilesResponse { get; set; }
        public PaymentType PaymentType { get; set; }
        public string orderId { get; set; }
    }
}
