using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace IBE.Client.FlightFilter
{
    [DataContract]
    [Serializable]
    public class FilterCriteria
    {
        #region Private Variables
        private List<string> mlobjAirlines = new List<string>();
        private List<string> mlobjFilteredAirlines = new List<string>();
        private List<int> mlobjNoofStops= new List<int>();
        private bool mblAllAirlines;
        private int mintMinPrice;
        private int mintMaxPrice;
        private int mintMinPoints;
        private int mintMaxPoints;
        private int mintNoOFStops;
        private int mintMinDepTime;
        private int mintMaxDepTime;
        private int mintMinArrTime;
        private int mintMaxArrTime;
        
        private int mintMinDuration;
        private int mintMaxDuration;

        private int mintCurrentMinDuration;
        private int mintCurrentMaxDuration;
        private int mintPageSize;

        private int mintCurrentMinPrice;
        private int mintCurrentMaxPrice;
        private int mintCurrentMinPoints;
        private int mintCurrentMaxPoints;
        private int mintCurrentMinDepTime;
        private int mintCurrentMaxDepTime;
        private int mintCurrentMinArrTime;
        private int mintCurrentMaxArrTime;

        #endregion

        #region Properties

        /// <summary>
        /// Airlines
        /// </summary>
        [DataMember]
        public List<string> Airlines
        {
            get { return mlobjAirlines; }
            set { mlobjAirlines = value; }
        }


        /// <summary>
        /// Filtered Airlines
        /// </summary>
        [DataMember]
        public List<string> FilteredAirlines
        {
            get { return mlobjFilteredAirlines; }
            set { mlobjFilteredAirlines = value; }
        }

        /// <summary>
        /// Filtered Airlines
        /// </summary>
        [DataMember]
        public List<int> FilteredNoofStops
        {
            get { return mlobjNoofStops; }
            set { mlobjNoofStops = value; }
        }

        /// <summary>
        /// AllAirlines
        /// </summary>
        [DataMember]
        public bool AllAirlines
        {
            get { return mblAllAirlines; }
            set { mblAllAirlines = value; }
        }

        /// <summary>
        /// MinPrice
        /// </summary>
        [DataMember]
        public int MinPrice
        {
            get { return mintMinPrice; }
            set { mintMinPrice = value; }
        }

        /// <summary>
        /// MaxPrice
        /// </summary>
        [DataMember]
        public int MaxPrice
        {
            get { return mintMaxPrice; }
            set { mintMaxPrice = value; }
        }

        /// <summary>
        /// MinPoints
        /// </summary>
        [DataMember]
        public int MinPoints
        {
            get { return mintMinPoints; }
            set { mintMinPoints = value; }
        }

        /// <summary>
        /// MaxPoints
        /// </summary>
        [DataMember]
        public int MaxPoints
        {
            get { return mintMaxPoints; }
            set { mintMaxPoints = value; }
        }

        /// <summary>
        /// NoOFStops
        /// </summary>
        [DataMember]
        public int NoOFStops
        {
            get { return mintNoOFStops; }
            set { mintNoOFStops = value; }
        }

        /// <summary>
        /// MinDepTime
        /// </summary>
        [DataMember]
        public int MinDepTime
        {
            get { return mintMinDepTime; }
            set { mintMinDepTime = value; }
        }

        /// <summary>
        /// MaxDepTime
        /// </summary>
        [DataMember]
        public int MaxDepTime
        {
            get { return mintMaxDepTime; }
            set { mintMaxDepTime = value; }
        }

        /// <summary>
        /// MinArrTime
        /// </summary>
        [DataMember]
        public int MinArrTime
        {
            get { return mintMinArrTime; }
            set { mintMinArrTime = value; }
        }

        /// <summary>
        /// MaxArrTime
        /// </summary>
        [DataMember]
        public int MaxArrTime
        {
            get { return mintMaxArrTime; }
            set { mintMaxArrTime = value; }
        }

        /// <summary>
        /// CurrentMinPrice
        /// </summary>
        [DataMember]
        public int CurrentMinPrice
        {
            get { return mintCurrentMinPrice; }
            set { mintCurrentMinPrice = value; }
        }

        /// <summary>
        /// CurrentMaxPrice
        /// </summary>
        [DataMember]
        public int CurrentMaxPrice
        {
            get { return mintCurrentMaxPrice; }
            set { mintCurrentMaxPrice = value; }
        }

        /// <summary>
        /// CurrentMinPoints
        /// </summary>
        [DataMember]
        public int CurrentMinPoints
        {
            get { return mintCurrentMinPoints; }
            set { mintCurrentMinPoints = value; }
        }

        /// <summary>
        /// CurrentMaxPoints
        /// </summary>
        [DataMember]
        public int CurrentMaxPoints
        {
            get { return mintCurrentMaxPoints; }
            set { mintCurrentMaxPoints = value; }
        }

        /// <summary>
        /// CurrentMinDepTime
        /// </summary>
        [DataMember]
        public int CurrentMinDepTime
        {
            get { return mintCurrentMinDepTime; }
            set { mintCurrentMinDepTime = value; }
        }

        /// <summary>
        /// CurrentMaxDepTime
        /// </summary>
        [DataMember]
        public int CurrentMaxDepTime
        {
            get { return mintCurrentMaxDepTime; }
            set { mintCurrentMaxDepTime = value; }
        }

        /// <summary>
        /// CurrentMinArrTime
        /// </summary>
        [DataMember]
        public int CurrentMinArrTime
        {
            get { return mintCurrentMinArrTime; }
            set { mintCurrentMinArrTime = value; }
        }

        /// <summary>
        /// CurrentMaxArrTime
        /// </summary>
        [DataMember]
        public int CurrentMaxArrTime
        {
            get { return mintCurrentMaxArrTime; }
            set { mintCurrentMaxArrTime = value; }
        }

        /// <summary>
        /// MinDuration
        /// </summary>
        [DataMember]
        public int MinDuration
        {
            get { return mintMinDuration; }
            set { mintMinDuration = value; }
        }

        /// <summary>
        /// MaxDuration
        /// </summary>
        [DataMember]
        public int MaxDuration
        {
            get { return mintMaxDuration; }
            set { mintMaxDuration = value; }
        }

        /// <summary>
        /// CurrentMinDuration
        /// </summary>
        [DataMember]
        public int CurrentMinDuration
        {
            get { return mintCurrentMinDuration; }
            set { mintCurrentMinDuration = value; }
        }

        /// <summary>
        /// CurrentMaxDuration
        /// </summary>
        [DataMember]
        public int CurrentMaxDuration
        {
            get { return mintCurrentMaxDuration; }
            set { mintCurrentMaxDuration = value; }
        }

        /// <summary>
        /// PageSize
        /// </summary>
        [DataMember]
        public int PageSize
        {
            get { return mintPageSize; }
            set { mintPageSize = value; }
        }

        

        #endregion
    }
}
