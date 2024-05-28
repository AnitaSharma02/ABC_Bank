using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [DataContract]
    [Serializable]
    public class FilterFlightRange
    {
        private int mintMaxPoint = 0;
        private int mintMinPoint = 0;
        private int mintMinDuration = 0;
        private int mintMaxDuration = 0;
        private List<string> lobjListOfUniqueAirlines = new List<string>();
        private int lobjListOfStops = 0;

        [DataMember]
        public int MaxPoint { get { return mintMaxPoint; } set { mintMaxPoint = value; } }
        [DataMember]
        public int MinPoint { get { return mintMinPoint; } set { mintMinPoint = value; } }
        [DataMember]
        public int MinDuration { get { return mintMinDuration; } set { mintMinDuration = value; } }
        [DataMember]
        public int MaxDuration { get { return mintMaxDuration; } set { mintMaxDuration = value; } }
        [DataMember]
        public List<string> Airlines { get { return lobjListOfUniqueAirlines; } set { lobjListOfUniqueAirlines = value; } }
        [DataMember]
        public int Stops { get { return lobjListOfStops; } set { lobjListOfStops = value;} }


    }
}
