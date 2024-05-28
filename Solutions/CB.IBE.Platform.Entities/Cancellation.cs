using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public partial class Cancellation
    {

        private int txnidField=0;

        private float refundamountField=0.0F;

        private bool refundamountFieldSpecified;

        private float cancellationchargesField=0.0F;

        private bool cancellationchargesFieldSpecified;

        private string suppliercancellationField = string.Empty;

        [DataMember]
        public int txnid
        {
            get
            {
                return this.txnidField;
            }
            set
            {
                this.txnidField = value;
            }
        }

        [DataMember]
        public float refundamount
        {
            get
            {
                return this.refundamountField;
            }
            set
            {
                this.refundamountField = value;
            }
        }

        [DataMember]
        public bool refundamountSpecified
        {
            get
            {
                return this.refundamountFieldSpecified;
            }
            set
            {
                this.refundamountFieldSpecified = value;
            }
        }

        [DataMember]
        public float cancellationcharges
        {
            get
            {
                return this.cancellationchargesField;
            }
            set
            {
                this.cancellationchargesField = value;
            }
        }

        [DataMember]
        public bool cancellationchargesSpecified
        {
            get
            {
                return this.cancellationchargesFieldSpecified;
            }
            set
            {
                this.cancellationchargesFieldSpecified = value;
            }
        }

        [DataMember]
        public string suppliercancellation
        {
            get
            {
                return this.suppliercancellationField;
            }
            set
            {
                this.suppliercancellationField = value;
            }
        }
    }

}
