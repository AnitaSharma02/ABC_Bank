using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGDetails : PGBaseClass
    {
        public List<PGDetailsData> data { get; set; }
    }
}
