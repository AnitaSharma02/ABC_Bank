using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public partial class PricingSummary
    {
        private string feesField = string.Empty;

        private string basefareField = string.Empty;

        private string taxesField = string.Empty;

        private string totalfareField = string.Empty;

        private string mstrmarkupField = string.Empty;

        private string mstrDiscountField = string.Empty;

        private string mstrCashBackField = string.Empty;

        [DataMember]
        public string CashBack
        {
            get
            {
                return this.mstrCashBackField;
            }
            set
            {
                this.mstrCashBackField = value;
            }
        }

        [DataMember]
        public string Discount
        {
            get
            {
                return this.mstrDiscountField;
            }
            set
            {
                this.mstrDiscountField = value;
            }
        }

        [DataMember]
        public string basefare
        {
            get
            {
                return this.basefareField;
            }
            set
            {
                this.basefareField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string fees
        {
            get
            {
                return this.feesField;
            }
            set
            {
                this.feesField = value;
            }
        }


        /// <remarks/>
        /// 
        [DataMember]
        public string taxes
        {
            get
            {
                return this.taxesField;
            }
            set
            {
                this.taxesField = value;
            }
        }

        [DataMember]
        public string totalfare
        {
            get
            {
                return this.totalfareField;
            }
            set
            {
                this.totalfareField = value;
            }
        }

        /// <summary>
        /// Displays Markup for Domestic 
        /// </summary>
        [DataMember]
        public string Markup
        {
            get { return mstrmarkupField; }
            set { mstrmarkupField = value; }
        }

    }

}
