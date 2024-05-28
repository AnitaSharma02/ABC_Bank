using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class Countries
    {
        private string mstrCountryID = string.Empty;
        private string mstrCountryCode =string.Empty;
        private string mstrCountrName =string.Empty;
        private string mstrCode = string.Empty;
        private List<Cities> CitiesField = new List<Cities>();

        [DataMember]
        public string CountryId { get { return mstrCountryID; } set { mstrCountryID = value; } }

        [DataMember]
        public string CountryCode { get { return mstrCountryCode; } set { mstrCountryCode = value; } }

        [DataMember]
        public string CountryName { get { return mstrCountrName; } set { mstrCountrName = value; } }

        [DataMember]
        public string Code { get { return mstrCode; } set { mstrCode = value;} }

        [DataMember]
        public List<Cities> Cities { get { return CitiesField; } set { CitiesField = value; } }

    }
}
