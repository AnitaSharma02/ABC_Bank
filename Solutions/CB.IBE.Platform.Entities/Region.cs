using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class Region
    {
        private int RegionIDField = 0;
        private string RegionNameField = string.Empty;
        private List<Countries> CountriesField = new List<Countries>();
        [DataMember]
        public int RegionID 
        { 
            get { return RegionIDField; } 
            set { RegionIDField = value; } 
        }
        [DataMember]
        public string RegionName
        {
            get { return RegionNameField; }
            set { RegionNameField = value; }
        }
        [DataMember]
        public List<Countries> Countries
        {
            get { return CountriesField; }
            set { CountriesField = value; }
        }


    }
}
