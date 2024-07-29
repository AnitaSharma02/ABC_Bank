using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBEAPI.ClientEntities
{
    public class CarBookingData
    {
        public string CaruniqueRef { get; set; }
        public string rateReference { get; set; }
        public string CarHireAmount { get; set; }
        public string TotalAmount { get; set; }
        public string PayableAmount { get; set; }
        public string PayableonArivalAmount { get; set; }
        public string TotalAdditionalequipmentAmount { get; set; }
        public string TotalAdditionalchargesAmount { get; set; }
        public List<AdditonalCharges> AdditonalCharges { get; set; } = new List<AdditonalCharges>();
    }

    public class AdditonalCharges
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string TotalChargeamount { get; set; }
        public string amount { get; set; }
        public bool IsAdditionalEquipments { get; set; }
        public string Quantity { get; set; }
    }
}
