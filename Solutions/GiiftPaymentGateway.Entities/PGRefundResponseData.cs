using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGRefundResponseData
    {
       
        public string refundId { get; set; }
      
        public string refundAmount { get; set; }
      
        public string refundStatus { get; set; }
      
        public string dateAdded { get; set; }
       
        public string dateModified { get; set; }
    }
}
