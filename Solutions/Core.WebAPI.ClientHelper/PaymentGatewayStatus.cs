using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Core.WebAPI.ClientHelper
{
    [DataContract]
    [Serializable]
    [Flags]
    public enum PaymentGatewayStatus
    {
        [EnumMember]
        Success = 1,
        [EnumMember]
        Failed = 2,
        [EnumMember]
        Cancelled = 3,
        [EnumMember]
        InProcess = 4
    }
}
