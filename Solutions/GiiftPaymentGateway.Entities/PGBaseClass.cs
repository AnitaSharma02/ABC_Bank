using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GiiftPaymentGateway.Entities
{
    public class PGBaseClass
    {
        public int code { get; set; }
        public int success { get; set; }
        public string message { get; set; }
        public string errorId { get; set; }
        public string errorInfo { get; set; }
        public string error { get; set; }
    }
}
