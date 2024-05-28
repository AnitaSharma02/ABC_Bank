using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGRefundRequest
    {
        public string orderId { get; set; }
        public int refundAmount { get; set; }
        public string refundNote { get; set; }
    }
}
