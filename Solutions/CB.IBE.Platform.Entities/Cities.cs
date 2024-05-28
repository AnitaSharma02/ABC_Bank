using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class Cities
    {
        private string mstrCityId = string.Empty;
        private  string mstrCountryId = string.Empty;
        private string mstrCityCode = string.Empty;
        private string mstrCityName = string.Empty;
        private string mstrIATACode = string.Empty;

        [DataMember]
        public string CityId { get { return mstrCityId; } set { mstrCityId = value; } }

        [DataMember]
        public string CountryId { get { return mstrCountryId; } set { mstrCountryId = value;} }

        [DataMember]
        public string CityCode { get { return mstrCityCode; } set { mstrCityCode = value; } }

        [DataMember]
        public string IATACode { get { return mstrIATACode; } set { mstrIATACode = value; } }

        [DataMember]
        public string CityName { get { return mstrCityName; } set { mstrCityName = value; } }
    }
}
