using System.Collections.Generic;

namespace IBEAPI.ClientEntities
{
    public class CarCountryResponse
    {
        public int success { get; set; }
        public List<CarCountryList> data { get; set; }
    }
}