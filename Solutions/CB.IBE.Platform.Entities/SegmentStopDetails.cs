using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class SegmentStopDetails
    {
        #region Private Variables

        private string mstrLayoverAirport = string.Empty;

        private DateTime mdtDepartureDatetime = DateTime.MinValue.ToUniversalTime();

        private DateTime mdtArrivalDatetime = DateTime.MinValue.ToUniversalTime();

        private int mintLayoverDuration = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Displays Layover Airport
        /// </summary>
        [DataMember]
        public string LayoverAirport
        {
            get { return mstrLayoverAirport; }
            set { mstrLayoverAirport = value; }
        }

        /// <summary>
        /// Displays Departure Datetime
        /// </summary>
        [DataMember]
        public DateTime DepartureDatetime
        {
            get { return mdtDepartureDatetime; }
            set { mdtDepartureDatetime = value; }
        }

        [DataMember]
        public string DepartureDate
        {
            get { return mdtDepartureDatetime.ToString("dd-MM-yyyy"); }
            set { }
        }

        /// <summary>
        /// Displays Arrival Datetime
        /// </summary>
        [DataMember]
        public DateTime ArrivalDatetime
        {
            get { return mdtArrivalDatetime; }
            set { mdtArrivalDatetime = value; }
        }

        [DataMember]
        public string ArrivalDate
        {
            get { return mdtArrivalDatetime.ToString("dd-MM-yyyy"); }
            set { }
        }

        /// <summary>
        /// Displays Layover Duration
        /// </summary>
        [DataMember]
        public int LayoverDuration
        {
            get { return mintLayoverDuration; }
            set { mintLayoverDuration = value; }
        }

        [DataMember]
        public string DisplayLayoverDuration
        {
            get { return ((mintLayoverDuration / 60) / 60) + "h " + ((mintLayoverDuration / 60) % 60)+"m"; }
            set { }
        }

        #endregion
    }
}
