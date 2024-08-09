using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ValidateCVMemberResponse
    {
        [DataMember]
        public string uniqueId { get; set; }
        [DataMember]
        public string ffid { get; set; }
        [DataMember]
        public string emailId { get; set; }
        [DataMember]
        public string mobileNo { get; set; }
        [DataMember]
        public string statusCode { get; set; }
        [DataMember]
        public string status { get; set; }
    }
}
