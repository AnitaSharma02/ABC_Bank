using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiInsurance.Entities
{
    public class InsuranceServiceProvidersResponse
    {
        public List<InsuranceSearchData> results { get; set; }
    }
    public class InsuranceSearchData
    {
        public string ServiceName { get; set; }
        public string ServiceCode { get; set; }
        public string ImageUrl { get; set; }
    }
}
