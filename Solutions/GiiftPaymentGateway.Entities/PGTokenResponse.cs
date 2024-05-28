using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGTokenResponse : PGBaseClass
    {
        public List<PGTokenResponseData> data { get; set; }
    }
}
