using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class BookingInfo
    {
        private string indexField = string.Empty;

        private string segmentindexField = string.Empty;

        private string pricinginfoindexField = string.Empty;

        private string bookingclassField = string.Empty;

        private string cabintypeField = string.Empty;

        private string tickettypeField = string.Empty;

        private string airlinepnrField = string.Empty;

        private string bookingstatusField = string.Empty;

        private string gdspnrField = string.Empty;

        private string ticketnumberField = string.Empty;

        
        /// <remarks/>
        /// 
        [DataMember]
        public string index
        {
            get
            {
                return this.indexField;
            }
            set
            {
                this.indexField = value;
            }
        }

        [DataMember]
        public string segmentindex
        {
            get
            {
                return this.segmentindexField;
            }
            set
            {
                this.segmentindexField = value;
            }
        }

        
        [DataMember]
        public string pricinginfoindex
        {
            get
            {
                return this.pricinginfoindexField;
            }
            set
            {
                this.pricinginfoindexField = value;
            }
        }

        
        [DataMember]
        public string bookingclass
        {
            get
            {
                return this.bookingclassField;
            }
            set
            {
                this.bookingclassField = value;
            }
        }

        
        [DataMember]
        public string cabintype
        {
            get
            {
                return this.cabintypeField;
            }
            set
            {
                this.cabintypeField = value;
            }
        }

        
        [DataMember]
        public string tickettype
        {
            get
            {
                return this.tickettypeField;
            }
            set
            {
                this.tickettypeField = value;
            }
        }

        [DataMember]
        public string airlinepnr
        {
            get
            {
                return this.airlinepnrField;
            }
            set
            {
                this.airlinepnrField = value;
            }
        }

        [DataMember]
        public string bookingStatus { get { return bookingstatusField; } set { bookingstatusField = value; } }


         [DataMember]
        public string gdspnr
        {
            get { return gdspnrField; }
            set { gdspnrField = value; }
        }

         [DataMember]
        public string ticketnumber
        {
            get { return ticketnumberField; }
            set { ticketnumberField = value; }
        }
    }
}
