using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class AirField
    {
        private string mstrCityName=string.Empty;
        private string mstrCountryName = string.Empty;
        private string mstrCountryCode = string.Empty;
        private string mstrCityIATACode = string.Empty;
        private string mstrRegionName = string.Empty;
        private string mstrContinentName = string.Empty;
        private string mstrTerminal = string.Empty;
        private string SearchTextField = string.Empty;
        private bool mbIsActive = false;
        private string mstrAirportName = string.Empty;
        private string mstrlatitude = string.Empty;
        private string mstrlongitude = string.Empty;
        private string mstrtimezone = string.Empty;
        //private string AirFieldDetailDescriptionField = "<b>{0}</b><br/>{1}"; //city,iatacode,countryname

        [DataMember]
        public string City 
        { 
            get { return mstrCityName; } 
            set { mstrCityName = value; }
        }
        [DataMember]
        public string CountryName 
        { 
            get { return mstrCountryName; } 
            set { mstrCountryName = value; } 
        }
        [DataMember]
        public string CountryCode 
        { 
            get { return mstrCountryCode; } 
            set { mstrCountryCode = value; } 
        }
        [DataMember]
        public string IATACode 
        { 
            get { return mstrCityIATACode; } 
            set { mstrCityIATACode = value; } 
        }
        [DataMember]
        public string Region 
        { 
            get { return mstrRegionName; } 
            set { mstrRegionName = value; } 
        }
        [DataMember]
        public string Continent { get { return mstrContinentName; } set { mstrRegionName = value; } }

        [DataMember]
        public string Terminal
        {
            get { return mstrTerminal; }
            set { mstrTerminal = value; }
        }
        [DataMember]
        public string AirportName
        {
            get { return mstrAirportName; }
            set { mstrAirportName = value; }
        }
        //[DataMember]
        //public string AirFieldDescription
        //{
        //    get { return string.Format(AirFieldDetailDescriptionField, mstrCityName, mstrAirportName); }
        //    set {  }
        //}
        [DataMember]
        public bool IsActive
        {
            get { return mbIsActive; }
            set { mbIsActive = value; }
        }


        [DataMember]
        public string SearchAirfieldDetails
        {
            get
            {
                //return getAirfieldDetails(this.AirfieldName); 
                return IATACode + ", " + AirportName + ", " + City + ", " + CountryName.ToUpper();
            }
            set { }
        }

        /// <summary>
        /// timezone
        /// </summary>
        [DataMember]
        public string Timezone
        {
            get { return mstrtimezone; }
            set { mstrtimezone = value; }
        }

        /// <summary>
        /// latitude
        /// </summary>
        [DataMember]
        public string Latitude
        {
            get { return mstrlatitude; }
            set { mstrlatitude = value; }
        }

        /// <summary>
        /// longitude
        /// </summary>
        [DataMember]
        public string Longitude
        {
            get { return mstrlongitude; }
            set { mstrlongitude = value; }
        }
    }
}
