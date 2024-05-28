using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGRequest
    {
        public int orderAmount { get; set; }
        public string orderCurrency { get; set; }
        public string orderId { get; set; }
        public PGCustomerDetails customerDetails { get; set; }
        public PGOrderMeta orderMeta { get; set; }
    }
    public class PGCustomerDetails
    {
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhone { get; set; }
    }
    public class PGOrderMeta
    {
        public string returnUrl { get; set; }
    }

}
