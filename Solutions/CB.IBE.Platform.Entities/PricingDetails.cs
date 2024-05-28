using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class PricingDetails
    {
        private string currencyField = string.Empty;
        private string refundableInfoField = string.Empty;
        private string aircraftType = string.Empty;
        private decimal adultPriceField = 0;
        private decimal adultTaxField = 0;
        private List<PaxTax> adultTaxesField = new List<PaxTax>();
        private decimal childPriceField = 0;
        private decimal childTaxField = 0;
        private List<PaxTax> childTaxesField = new List<PaxTax>();
        private decimal infantPriceField = 0;
        private decimal infantTaxField = 0;
        private List<PaxTax> infantTaxesField = new List<PaxTax>();
        private string searchIdField = string.Empty;
        private string flightIdField = string.Empty;
        private decimal serviceTaxField = 0;
        private decimal mSFTaxField = 0;
        private decimal cCFeeField = 0;
        private decimal miscFeeField = 0;
        private bool isLccField = false;
        private decimal transactionFeesField = 0;
        private string lastTkDt = string.Empty;
        

        /// <remarks/>
        /// 
        [DataMember]
        public string Currency
        {
            get
            {
                return this.currencyField;
            }
            set
            {
                this.currencyField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string RefundableInfo
        {
            get
            {
                return this.refundableInfoField;
            }
            set
            {
                this.refundableInfoField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string AircraftType
        {
            get { return aircraftType; }
            set { aircraftType = value; }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal AdultPrice
        {
            get
            {
                return this.adultPriceField;
            }
            set
            {
                this.adultPriceField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal AdultTax
        {
            get
            {
                return this.adultTaxField;
            }
            set
            {
                this.adultTaxField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public List<PaxTax> AdultTaxes
        {
            get
            {
                return this.adultTaxesField;
            }
            set
            {
                this.adultTaxesField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal ChildPrice
        {
            get
            {
                return this.childPriceField;
            }
            set
            {
                this.childPriceField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal ChildTax
        {
            get
            {
                return this.childTaxField;
            }
            set
            {
                this.childTaxField = value;
            }
        }


        [DataMember]
        public List<PaxTax> ChildTaxes
        {
            get
            {
                return this.childTaxesField;
            }
            set
            {
                this.childTaxesField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal InfantPrice
        {
            get
            {
                return this.infantPriceField;
            }
            set
            {
                this.infantPriceField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal InfantTax
        {
            get
            {
                return this.infantTaxField;
            }
            set
            {
                this.infantTaxField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public List<PaxTax> InfantTaxes
        {
            get
            {
                return this.infantTaxesField;
            }
            set
            {
                this.infantTaxesField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string SearchId
        {
            get
            {
                return this.searchIdField;
            }
            set
            {
                this.searchIdField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string FlightId
        {
            get
            {
                return this.flightIdField;
            }
            set
            {
                this.flightIdField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal ServiceTax
        {
            get
            {
                return this.serviceTaxField;
            }
            set
            {
                this.serviceTaxField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal MSFTax
        {
            get
            {
                return this.mSFTaxField;
            }
            set
            {
                this.mSFTaxField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal CCFee
        {
            get
            {
                return this.cCFeeField;
            }
            set
            {
                this.cCFeeField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal MiscFee
        {
            get
            {
                return this.miscFeeField;
            }
            set
            {
                this.miscFeeField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public bool IsLcc
        {
            get
            {
                return this.isLccField;
            }
            set
            {
                this.isLccField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public decimal TransactionFees
        {
            get
            {
                return this.transactionFeesField;
            }
            set
            {
                this.transactionFeesField = value;
            }
        }

        /// <remarks/>
        /// 
        [DataMember]
        public string LastTkDt
        {
            get { return lastTkDt; }
            set { lastTkDt = value; }
        }

    }
}
