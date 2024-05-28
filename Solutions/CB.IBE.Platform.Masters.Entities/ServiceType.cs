using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Masters.Entities
{

    [DataContract]
    [Serializable]
    [Flags]
    public enum ServiceType
    {
        [EnumMember]
        FLIGHT = 1,
        [EnumMember]
        HOTEL = 2,
        [EnumMember]
        CAR = 3,
        [EnumMember]
        LOUNGE = 4,
        [EnumMember]
        UTILITY = 5,
        [EnumMember]
        INSURANCE = 6,
        [EnumMember]
        TOUR = 7
    }
}
