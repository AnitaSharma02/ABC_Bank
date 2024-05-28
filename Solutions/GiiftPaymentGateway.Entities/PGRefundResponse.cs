using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGRefundResponse : PGBaseClass
    {
        public List<PGRefundResponseData> data { get; set; }
    }
}
