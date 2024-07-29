using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class RateRequest
    {
        public string lang { get; set; }
        public string rateReference { get; set; }
        public bool getTerms { get; set; }
        public string displayCurrency { get; set; }
        public bool debugMode { get; set; }
    }
}
