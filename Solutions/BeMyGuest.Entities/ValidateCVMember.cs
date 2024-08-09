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
    public class ValidateCVMember
    {
        [DataMember]
        public string uniqueId { get; set; }
        [DataMember]
        public string ffid { get; set; }
        [DataMember]
        public string emailId { get; set; }
        [DataMember]
        public string mobileNo { get; set; }
    }
}
