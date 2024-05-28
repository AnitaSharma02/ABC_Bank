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
    public enum AmendmentStatus
    {
        [EnumMember]
        Open = 1,
        [EnumMember]
        InProgress = 2,
        [EnumMember]
        Closed = 3
    }
}
