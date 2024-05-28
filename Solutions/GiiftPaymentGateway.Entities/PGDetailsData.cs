using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGDetailsData
    {
        public string paymentId { get; set; }

        public string orderId { get; set; }

        public string orderAmount { get; set; }

        public string orderStatus { get; set; }

        public PGCustomerDetails customerDetails { get; set; }

        public PGOrderMeta orderMeta { get; set; }

        public DateTime dateAdded { get; set; }

        public DateTime dateModified { get; set; }
    }
}
