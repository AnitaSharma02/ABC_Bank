using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CB.IBE.DomesticFlight.Entities
{
    public class AirFieldsForDomestic
    {
        public bool Status { get; set; }

        public List<DomesticFlightData> Sectors { get; set; }
    }
    public class DomesticFlightData
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public bool IsNational { get; set; } = false;
        public bool IsActive { get; set; } = false;
        public bool IsInternational { get; set; } = false;
    }
}
