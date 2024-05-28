using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace CB.IBE.Platform.Masters.Entities
{
    [DataContract]
    [Serializable]
    [Flags]
    public enum MethodType
    {
        [EnumMember]
        SEARCH = 1,
        [EnumMember]
        REVIEWCONFIRM = 2,
        [EnumMember]
        BOOKING = 3,
        [EnumMember]
        CANCELBOOKING = 4,
        [EnumMember]
        VIEWTRIP = 5

    }
}
