using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class Continent
    {
        private int ContinentIDField = 0;
        private string ContinetNameField = string.Empty;
        private List<Region> RegionField = new List<Region>();
        [DataMember]
        public int ContinentID
        { 
            get { return ContinentIDField; }
            set { ContinentIDField = value; }
        }
        [DataMember]
        public string ContinentName 
        { 
            get { return ContinetNameField; } 
            set { ContinetNameField = value; } 
        }
        [DataMember]
        public List<Region> Region 
        { 
            get { return RegionField; } 
            set { RegionField = value; } 
        }
    }
}
