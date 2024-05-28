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
    public enum DiscountType
    {
        [EnumMember]
        BaseFare = 1,
        [EnumMember]
        PerPassenger = 2,
        [EnumMember]
        FullTicket = 3


    }
}
