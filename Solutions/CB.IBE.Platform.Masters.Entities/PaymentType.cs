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
    public enum PaymentType
    {
        [EnumMember]
        None = 0,
        [EnumMember]
        Points = 1,
        [EnumMember]
        Cash = 2,
        [EnumMember]
        CashPoints = 3,
        [EnumMember]
        InfiVoucher = 4,
        [EnumMember]
        Other = 5,
        [EnumMember]
        VoucherPoints = 6,
        [EnumMember]
        VoucherCash = 7
    }
}
