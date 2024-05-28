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
    public enum MarkupType
    {
        [EnumMember]
        ALL = 1,
        [EnumMember]
        REVIEWCONFIRM = 2,
    }
}
