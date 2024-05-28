using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiISP.Entities
{
    public class ISPPaymentRequest
    {
       public string ServiceCode { get; set; }
       public decimal Amount { get; set; }
        public int SessionId { get; set; }
        public string RequestId { get; set; }
        public string PackageId { get; set; }
        public string DurationCode { get; set; }
        public string MembershipReference { get; set; }
        public string CustomerName { get; set; }
}
}
