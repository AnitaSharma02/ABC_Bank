using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class AirCraftDetails
    {
        private string mstrIATACode = string.Empty;
        private string mstrICAOCode = string.Empty;
        private string mstrAirCraftType = string.Empty;
        private string mstrWakeCategory = string.Empty;
        [DataMember]
        public string IATACode { get { return mstrIATACode; } set { mstrIATACode = value; } }
        [DataMember]
        public string ICAOCode { get { return mstrICAOCode; } set { mstrICAOCode = value; } }
        [DataMember]
        public string AirCraftType { get { return mstrAirCraftType; } set { mstrAirCraftType = value; } }
        [DataMember]
        public string WakeCategory { get { return mstrWakeCategory; } set { mstrWakeCategory = value; } }

    }
}
