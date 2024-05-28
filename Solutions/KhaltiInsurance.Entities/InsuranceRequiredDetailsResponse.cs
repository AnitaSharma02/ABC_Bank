using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KhaltiInsurance.Entities
{
    public class InsuranceRequiredDetailsResponse
    {
        public Results results { get; set; }
    }
    public class Results
    {
        public MandatoryFields fields { get; set; }
        public string format { get; set; }
    }

    public class MandatoryFields
    {
        public Fields PolicyNo { get; set; }=new Fields();
        public Fields DOB { get; set; } = new Fields();
        public Fields RequestId { get; set; } =new Fields();
        public Fields CustomerId { get; set; } =new Fields();
        public Fields Username { get; set; } = new Fields();
    }
    public class Fields
    {
        public string Type { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Pattern { get; set; } = string.Empty;
        public bool Required { get; set; }=false;
    }
}
