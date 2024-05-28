using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public enum SupplierCancellation
    {

        /// <remarks/>
        /// 
        [EnumMember]
        S,

        /// <remarks/>
        /// 
        [EnumMember]
        F,
    }
}
