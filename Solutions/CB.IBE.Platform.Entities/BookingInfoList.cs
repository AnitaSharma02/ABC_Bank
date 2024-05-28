using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace CB.IBE.Platform.Entities
{
    [Serializable]
    [DataContract]
    public class BookingInfoList
    {
        private List<BookingInfo> bookingInfoField;

        [DataMember]
        public List<BookingInfo> BookingInfo { get { return bookingInfoField; } set { bookingInfoField = value; } }
    }
}
