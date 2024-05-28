using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiISP.Entities
{
    public class GetDiscountRequest
    {
        public string ServiceCode { get; set; }
        public int SessionId { get; set; }
        public Packages Package { get; set; }
    }
 
}
