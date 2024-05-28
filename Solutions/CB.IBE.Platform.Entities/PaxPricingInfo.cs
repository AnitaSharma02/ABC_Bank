using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PaxPricingInfo
    {
        private string paxtypeField = string.Empty;

        private PricingInfoList pricingInfoListField = new PricingInfoList();

        private BookingInfoList bookingInfoListField = new BookingInfoList();

        [DataMember]
        public string paxtype
        {
            get
            {
                return this.paxtypeField;
            }
            set
            {
                this.paxtypeField = value;
            }
        }

        [DataMember]
        public PricingInfoList PricingInfoList
        {
            get
            {
                return this.pricingInfoListField;
            }
            set
            {
                this.pricingInfoListField = value;
            }
        }

        [DataMember]
        public BookingInfoList BookingInfoList
        {
            get
            {
                return this.bookingInfoListField;
            }
            set
            {
                this.bookingInfoListField = value;
            }
        }

    }
}
